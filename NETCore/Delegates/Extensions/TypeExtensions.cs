// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TypeExtensions.cs" company="Natan Podbielski">
//   Copyright (c) 2016 - 2016 Natan Podbielski. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using static Delegates.Helper.ParametersIndexes;

namespace Delegates.Extensions
{
    internal static class TypeExtensions
    {
        private const string Item = "Item";

        public const string AddAccessor = "add";

        public const string RemoveAccessor = "remove";

        private const BindingFlags PrivateOrProtectedBindingFlags = BindingFlags.NonPublic;

        private const BindingFlags InternalBindingFlags = BindingFlags.Public | BindingFlags.NonPublic;

        public static bool CanBeAssignedFrom(this Type destination, Type source)
        {
            if (source == null || destination == null)
            {
                return false;
            }

            if (destination == source || source.GetTypeInfo().IsSubclassOf(destination))
            {
                return true;
            }

            if (destination.GetTypeInfo().IsInterface)
            {
                return source.ImplementsInterface(destination);
            }

            if (!destination.IsGenericParameter)
            {
                return false;
            }

            var validNewConstraint = IsNewConstraintValid(destination, source);
            var validReferenceConstraint = IsReferenceConstraintValid(destination, source);
            var validValueTypeConstraint = IsValueConstraintValid(destination, source);
            var constraints = destination.GetTypeInfo().GetGenericParameterConstraints();
            return validNewConstraint && validReferenceConstraint && validValueTypeConstraint &&
                constraints.All(t1 => t1.CanBeAssignedFrom(source));
        }

        public static bool IsCrossConstraintInvalid(this Type source, Type[] allGenericArgs, Type[] typeParameters)
        {
            var constraints = source.GetTypeInfo().GetGenericParameterConstraints();
            var invalid = false;
            foreach (var constraint in constraints)
            {
                //if constraint is in collection of other generic parameters types definitions -> cross constraint; check inheritance
                var indexOf = Array.IndexOf(allGenericArgs, constraint);
                if (indexOf > -1)
                {
                    var sourceTypeParameter = typeParameters[Array.IndexOf(allGenericArgs, source)];
                    var constraintTypeParameter = typeParameters[indexOf];
                    if (!constraintTypeParameter.CanBeAssignedFrom(sourceTypeParameter))
                    {
                        invalid = true;
                        break;
                    }
                }
            }
            return invalid;
        }

        private static bool IsNewConstraintValid(Type destination, Type source)
        {
            //check new() cosntraint which is not included in GetGenericParameterConstraints
            var valid = destination.GetTypeInfo().GenericParameterAttributes ==
                                     GenericParameterAttributes.DefaultConstructorConstraint;
            if (valid)
            {
                valid &= source.GetTypeInfo().GetConstructor(new Type[0]) != null;
            }
            else
            {
                valid = true;
            }
            return valid;
        }

        private static bool IsReferenceConstraintValid(Type destination, Type source)
        {
            //check class cosntraint which is not included in GetGenericParameterConstraints
            var valid = destination.GetTypeInfo().GenericParameterAttributes ==
                                     GenericParameterAttributes.ReferenceTypeConstraint;
            if (valid)
            {
                valid &= source.GetTypeInfo().IsClass;
            }
            else
            {
                valid = true;
            }
            return valid;
        }

        private static bool IsValueConstraintValid(Type destination, Type source)
        {
            //check new() cosntraint which is not included in GetGenericParameterConstraints
            var valid = destination.GetTypeInfo().GenericParameterAttributes ==
                                     GenericParameterAttributes.NotNullableValueTypeConstraint;
            if (valid)
            {
                valid &= source.GetTypeInfo().IsValueType;
            }
            else
            {
                valid = true;
            }
            return valid;
        }

        private static bool ImplementsInterface(this Type source, Type interfaceType)
        {
            while (source != null)
            {
                var interfaces = source.GetTypeInfo().GetInterfaces();
                if (interfaces.Any(i => i == interfaceType
                                        || i.ImplementsInterface(interfaceType)))
                {
                    return true;
                }

                source = source.GetTypeInfo().BaseType;
            }
            return false;
        }

        public static Type[] GenericTypeArguments(this Type source)
        {
#if NET35||NET4||PORTABLE
            return source.GetGenericArguments();
#elif NET45||NETCORE
            return source.GetTypeInfo().GenericTypeArguments;
#endif
        }

        public static bool IsClassType(this Type source)
        {
#if NET35||NET4||PORTABLE
            return source.IsClass;
#elif NET45||NETCORE
            return source.GetTypeInfo().IsClass;
#endif
        }

        public static bool IsValueType(this Type source)
        {
#if NET35||NET4||PORTABLE
            return source.IsValueType;
#elif NET45||NETCORE
            return source.GetTypeInfo().IsValueType;
#endif
        }

#if NET45 || NETCORE
        public static MethodInfo GetMethod(this Type source, string methodName)
        {
            return source.GetTypeInfo().GetMethod(methodName);
        }
#endif

#if NET35||NET4||PORTABLE
        public static Type GetTypeInfo(this Type source)
        {
            return source;
        }
#endif

        public static List<ParameterExpression> GetParamsExprFromTypes(this Type[] types)
        {
#if NET35
            var index = 0;
#endif
            var parameters = types
#if NET35
                .Select(a => Expression.Parameter(a, "p" + index++))
#elif NET45 || NETCORE || NET4||PORTABLE
                .Select(Expression.Parameter)
#endif
                .ToList();
            return parameters;
        }

        public static MethodInfo GetGenericMethod(this Type source, string name, Type[] parametersTypes, Type[] typeParameters,
            bool isStatic)
        {
            MethodInfo methodInfo = null;
            var staticOrInstance = isStatic ? BindingFlags.Static : BindingFlags.Instance;
            var ms = source.GetTypeInfo().GetMethods(staticOrInstance | BindingFlags.Public)
                .Concat(source.GetTypeInfo().GetMethods(staticOrInstance | PrivateOrProtectedBindingFlags))
                .Concat(source.GetTypeInfo().GetMethods(staticOrInstance | InternalBindingFlags));
            foreach (var m in ms)
            {
                if (m.Name == name && m.IsGenericMethod)
                {
                    var parameters = m.GetParameters();
                    var genericArguments = m.GetGenericArguments();
                    var parametersTypesValid = parameters.Length == parametersTypes.Length;
                    parametersTypesValid &= genericArguments.Length == typeParameters.Length;
                    if (!parametersTypesValid)
                    {
                        continue;
                    }
                    for (var index = 0; index < parameters.Length; index++)
                    {
                        var parameterInfo = parameters[index];
                        var parameterType = parametersTypes[index];
                        if (parameterInfo.ParameterType != parameterType
                            && parameterInfo.ParameterType.IsGenericParameter
                            && !parameterInfo.ParameterType.CanBeAssignedFrom(parameterType))
                        {
                            parametersTypesValid = false;
                            break;
                        }
                    }
                    for (var index = 0; index < genericArguments.Length; index++)
                    {
                        var genericArgument = genericArguments[index];
                        var typeParameter = typeParameters[index];
                        if (!genericArgument.CanBeAssignedFrom(typeParameter)
                            //check cross parameters constraints
                            || genericArgument.IsCrossConstraintInvalid(genericArguments, typeParameters))
                        {
                            parametersTypesValid = false;
                            break;
                        }
                    }
                    if (parametersTypesValid)
                    {
                        methodInfo = m.MakeGenericMethod(typeParameters);
                        break;
                    }
                }
            }
            return methodInfo;
        }

        public static MethodInfo GetMethodInfoByEnumerate(this Type source, string name, Type[] parametersTypes,
            bool isStatic)
        {
            MethodInfo methodInfo = null;
            var staticOrInstance = isStatic ? BindingFlags.Static : BindingFlags.Instance;
            IEnumerable<MethodInfo> methods = source.GetTypeInfo().GetMethods(staticOrInstance | BindingFlags.Public);
            methods = methods.Concat(source.GetTypeInfo().GetMethods(staticOrInstance | PrivateOrProtectedBindingFlags));
            methods = methods.Concat(source.GetTypeInfo().GetMethods(staticOrInstance | InternalBindingFlags));
            var correctNameMethods = methods.Where(m => m.Name == name && !m.IsGenericMethod);
            foreach (var method in correctNameMethods)
            {
                var parameters = method.GetParameters();
                var parametersTypesValid = parameters.Length == parametersTypes.Length;
                if (!parametersTypesValid)
                {
                    continue;
                }
                for (var index = 0; index < parameters.Length; index++)
                {
                    var parameterInfo = parameters[index];
                    var parameterType = parametersTypes[index];
                    if (parameterInfo.ParameterType != parameterType)
                    {
                        parametersTypesValid = false;
                        break;
                    }
                }
                if (parametersTypesValid)
                {
                    methodInfo = method;
                    break;
                }
            }
            return methodInfo;
        }

        public static MethodInfo GetMethodInfo(this Type source, string name, Type[] parametersTypes,
              Type[] typeParameters = null, bool isStatic = false)
        {
            MethodInfo methodInfo = null;
            if (typeParameters == null || typeParameters.Length == 0)
            {
#if !(NETCORE || PORTABLE)
                var enumerateMethods = false;
                try
                {
                    methodInfo = source.GetSingleMethod(name, parametersTypes, isStatic);
                }
                catch (AmbiguousMatchException)
                {
                    //if more than one method it means there are also generic -> enumerate
                    enumerateMethods = true;
                }
                if (enumerateMethods)
                {
                    methodInfo = GetMethodInfoByEnumerate(source, name, parametersTypes, isStatic);
                }
#else
                methodInfo = GetMethodInfoByEnumerate(source, name, parametersTypes, isStatic);
#endif
            }
            //check for generic methods
            else
            {
                methodInfo = GetGenericMethod(source, name, parametersTypes, typeParameters, isStatic);
            }
            return methodInfo;
        }


#if !(NETCORE || PORTABLE)
        public static MethodInfo GetSingleMethod(this Type source, string name, Type[] parametersTypes, bool isStatic)
        {
            var staticOrInstance = isStatic ? BindingFlags.Static : BindingFlags.Instance;
            var methodInfo = (source.GetTypeInfo()
                     .GetMethod(name, staticOrInstance | BindingFlags.Public, null, parametersTypes, null) ??
                 source.GetTypeInfo()
                     .GetMethod(name, staticOrInstance | PrivateOrProtectedBindingFlags, null, parametersTypes,
                         null)) ??
                source.GetTypeInfo()
                    .GetMethod(name, staticOrInstance | InternalBindingFlags, null, parametersTypes, null);
            return methodInfo;
        }
#endif

        public static
#if NET35 || NET4 || PORTABLE
            CPropertyInfo
#else
            PropertyInfo
#endif
            GetPropertyInfo(this Type source, string propertyName, bool isStatic)
        {
            var staticOrInstance = isStatic ? BindingFlags.Static : BindingFlags.Instance;
            var propertyInfo = source.GetTypeInfo().GetProperty(propertyName, staticOrInstance) ??
                               source.GetTypeInfo().GetProperty(propertyName, staticOrInstance | PrivateOrProtectedBindingFlags) ??
                               source.GetTypeInfo().GetProperty(propertyName, staticOrInstance | InternalBindingFlags);
#if NET35 || NET4 || PORTABLE
            return new CPropertyInfo(propertyInfo);
#else
            return propertyInfo;
#endif
        }

        public static FieldInfo GetFieldInfo(this Type source, string fieldName, bool isStatic)
        {
            var staticOrInstance = isStatic ? BindingFlags.Static : BindingFlags.Instance;
            var fieldInfo = (source.GetTypeInfo().GetField(fieldName, staticOrInstance) ??
                             source.GetTypeInfo().GetField(fieldName, staticOrInstance | PrivateOrProtectedBindingFlags)) ??
                            source.GetTypeInfo().GetField(fieldName, staticOrInstance | InternalBindingFlags);
            return fieldInfo;
        }

        public static
#if NET35||NET4||PORTABLE
            CPropertyInfo
#else
            PropertyInfo
#endif
            GetIndexerPropertyInfo(this Type source, Type[] indexesTypes,
                string indexerName = null)
        {
            indexerName = indexerName ?? Item;
            var properties = source.GetTypeInfo().GetProperties().Concat(
                    source.GetTypeInfo().GetProperties(BindingFlags.NonPublic)).Concat(
                    source.GetTypeInfo().GetProperties(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance))
                .ToArray();
            if (indexerName == Item)
            {
                var firstIndexerInfo = properties.FirstOrDefault(p => p.GetIndexParameters().Length > 0);
                if (firstIndexerInfo != null && firstIndexerInfo.Name != indexerName)
                {
                    indexerName = firstIndexerInfo.Name;
                }
            }
            var indexerInfo = properties.FirstOrDefault(p => p.Name == indexerName
                                                             &&
                                                             IndexParametersEquals(p.GetIndexParameters(), indexesTypes));
            if (indexerInfo != null)
            {
#if NET35||NET4||PORTABLE
                return new CPropertyInfo(indexerInfo);
#else
                return indexerInfo;
#endif
            }
            return null;
        }

        public static ConstructorInfo GetConstructorInfo(this Type source, Type[] types)
        {
#if NETCORE||PORTABLE
            ConstructorInfo constructor = null;
            var constructors = source.GetTypeInfo().GetConstructors(BindingFlags.Public);
            if (!constructors.Any())
            {
                constructors = source.GetTypeInfo().GetConstructors(PrivateOrProtectedBindingFlags);
            }
            if (!constructors.Any())
            {
                constructors =
                    source.GetTypeInfo()
                        .GetConstructors(InternalBindingFlags | BindingFlags.Instance);
            }
            foreach (var c in constructors)
            {
                var parameters = c.GetParameters();
                var parametersTypesValid = parameters.Length == types.Length;
                if (!parametersTypesValid)
                {
                    continue;
                }
                for (var index = 0; index < parameters.Length; index++)
                {
                    var parameterInfo = parameters[index];
                    var parameterType = types[index];
                    if (parameterInfo.ParameterType != parameterType)
                    {
                        parametersTypesValid = false;
                        break;
                    }
                }
                if (parametersTypesValid)
                {
                    constructor = c;
                    break;
                }
            }
            return constructor;
#else
            return (source.GetConstructor(BindingFlags.Public, null, types, null) ??
                    source.GetConstructor(BindingFlags.NonPublic, null, types, null)) ??
                    source.GetConstructor(
                       InternalBindingFlags | BindingFlags.Instance, null,
                       types, null);
#endif
        }

        public static
#if NET35||NET4||PORTABLE
            CEventInfo
#else
            EventInfo
#endif
            GetEventInfo(this Type sourceType, string eventName)
        {
            var eventInfo = (sourceType.GetTypeInfo().GetEvent(eventName)
                             ?? sourceType.GetTypeInfo().GetEvent(eventName, PrivateOrProtectedBindingFlags))
                            ?? sourceType.GetTypeInfo().GetEvent(eventName,
                                InternalBindingFlags | BindingFlags.Instance);
#if NET35||NET4||PORTABLE
            return eventInfo != null ? new CEventInfo(eventInfo) : null;
#else
            return eventInfo;
#endif
        }

        public static MethodInfo GetEventAccessor(this Type sourceType, string eventName, string accessor)
        {
            var eventInfo = sourceType.GetEventInfo(eventName);
            return accessor == AddAccessor ? eventInfo?.AddMethod : eventInfo?.RemoveMethod;
        }
    }
}