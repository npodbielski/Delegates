// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TypeExtensions.cs" company="Natan Podbielski">
//   Copyright (c) 2016 - 2018 Natan Podbielski. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

#if NETCOREAPP1_0 || NETCOREAPP1_1 || NETCOREAPP2_0 || STANDARD
#define TaskUtilMethodsWith_TO_Suffix
#endif
#if !NETSTANDARD1_1
#define Supports_BindingFlags
#endif
#if NET45 || NET46 || NETCORE || STANDARD
#define Supports_GenericTypeArguments_Property
#endif
#if NETCOREAPP1_0 || NETCOREAPP1_1
#define Supports_GetMethod_OnlyWithNameAndBindingFlags
#endif
#if NET35 || NET4 || PORTABLE
#define Supports_EventAndProperty_AccessorsViaMethods
#define NotSupports_TypeInfo
#endif
#if !(PORTABLE || STANDARD)
#define NotSupports_GetMethod_WithNameBindingFlagsAndTypes
#endif
#if NETCORE || PORTABLE || STANDARD
#define NotSupports_GetConstructor_ByBindingFlagsAndParameterTypes
#endif

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
        private static Type _typeUtils;
        private static Func<Type, Type, bool> _hasIdentityPrimitiveOrNullableConversionDeleg;
        private static Func<Type, Type, bool> _hasReferenceConversionDeleg;
        private const string Item = "Item";

        public const string AddAccessor = "add";

        public const string RemoveAccessor = "remove";

#if Supports_BindingFlags
        private const BindingFlags PrivateOrProtectedBindingFlags = BindingFlags.NonPublic;

        private const BindingFlags InternalBindingFlags = BindingFlags.Public | BindingFlags.NonPublic;
#endif

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
            //check new() constraint which is not included in GetGenericParameterConstraints
            var valid = destination.GetTypeInfo().GenericParameterAttributes ==
                GenericParameterAttributes.DefaultConstructorConstraint;
            if (valid)
            {
                valid &= source.GetConstructor(new Type[0]) != null;
            }
            else
            {
                valid = true;
            }

            return valid;
        }

        private static bool IsReferenceConstraintValid(Type destination, Type source)
        {
            //check class constraint which is not included in GetGenericParameterConstraints
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
            //check new() constraint which is not included in GetGenericParameterConstraints
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

        private static Type[] GetInterfaces(this Type source)
        {
#if NETSTANDARD1_1
            return source.GetTypeInfo().ImplementedInterfaces.ToArray();
#else
            return source.GetTypeInfo().GetInterfaces();
#endif
        }

        private static bool ImplementsInterface(this Type source, Type interfaceType)
        {
            while (source != null)
            {
                var interfaces = source.GetInterfaces();
                if (interfaces.Any(i => i == interfaceType
                    || i.ImplementsInterface(interfaceType)))
                {
                    return true;
                }

                source = source.GetTypeInfo().BaseType;
            }

            return false;
        }

        public static Type[] GetGenericTypeArguments(this Type source)
        {
#if NET35 || NET4 || PORTABLE || NETCOREAPP1_0
            return source.GetGenericArguments();
#elif NET45 || NET46 || NETCORE || STANDARD
            return source.GetTypeInfo().GenericTypeArguments;
#endif
        }
        
        public static bool IsValidReturnType(this Type destination, Type source)
        {
            if (!source.CanBeAssignedFrom(destination)
                && !source.CanBeConvertedFrom(destination))
            {
                throw new ArgumentException(
                    $"Provided return type \'{source.Name}\' is not compatible with expected type " +
                    $"\'{destination.Name}\'");
            }

            return true;
        }

        public static bool CanBeConvertedFrom(this Type destination, Type source)
        {
            return HasIdentityPrimitiveOrNullableConversionDeleg(destination, source)
                || HasReferenceConversionDeleg(destination, source);
        }

        internal static Func<Type, Type, bool> HasReferenceConversionDeleg =>
            _hasReferenceConversionDeleg ??
            (_hasReferenceConversionDeleg = TypeUtils.StaticMethod<Func<Type, Type, bool>>
            (
#if TaskUtilMethodsWith_TO_Suffix
                "HasReferenceConversionTo"
#else
                "HasReferenceConversion"
#endif
            ));

        internal static Func<Type, Type, bool> HasIdentityPrimitiveOrNullableConversionDeleg =>
            _hasIdentityPrimitiveOrNullableConversionDeleg ??
            (_hasIdentityPrimitiveOrNullableConversionDeleg = TypeUtils.StaticMethod<Func<Type, Type, bool>>(
#if TaskUtilMethodsWith_TO_Suffix
                "HasIdentityPrimitiveOrNullableConversionTo"
#else
                "HasIdentityPrimitiveOrNullableConversion"
#endif
            ));

        internal static Type TypeUtils
        {
            get
            {
                var t = Type.GetType("System.Dynamic.Utils.TypeUtils, System.Linq.Expressions");
                return _typeUtils ?? (_typeUtils =
                            typeof(Expression)
                                .GetTypeInfo()
#if NET35
                                .Assembly.ImageRuntimeVersion.StartsWith("v4")?typeof(Expression).Assembly.GetType("System.Dynamic.Utils.TypeUtils"):typeof(Expression)
#else
                                .Assembly
#if NETSTANDARD1_1
                                .GetType("System.Dynamic.Utils.TypeUtils")
#else
                                .GetType("System.Dynamic.Utils.TypeUtils", true)
#endif
#endif
                    );
            }
        }

#if NET45 || NETSTANDARD1_5
        public static MethodInfo GetMethod(this Type source, string methodName)
        {
            var typeInfo = source.GetTypeInfo();
#if !NETSTANDARD1_5
            return typeInfo.GetMethod(methodName);
#else
            return typeInfo.GetDeclaredMethod(methodName);
#endif
        }
#endif

#if NotSupports_TypeInfo
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
#elif NET45 || NET46 || NETCORE || NET4 || PORTABLE || STANDARD
                .Select(Expression.Parameter)
#endif
                .ToList();
            return parameters;
        }

        private static IEnumerable<MethodInfo> GetAllMethods(this Type source, bool isStatic)
        {
#if NETSTANDARD1_1
            //TODO: filter by static or instance
            return source.GetTypeInfo().DeclaredMethods;
#else
            var staticOrInstance = isStatic ? BindingFlags.Static : BindingFlags.Instance;
            var ms = source.GetTypeInfo().GetMethods(staticOrInstance | BindingFlags.Public)
                .Concat(source.GetTypeInfo().GetMethods(staticOrInstance | PrivateOrProtectedBindingFlags))
                .Concat(source.GetTypeInfo().GetMethods(staticOrInstance | InternalBindingFlags));
            return ms;
#endif
        }

        public static MethodInfo GetGenericMethod(this Type source, string name, Type[] parametersTypes,
            Type[] typeParameters,
            bool isStatic)
        {
            MethodInfo methodInfo = null;
            var ms = source.GetAllMethods(isStatic);
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
            var methods = source.GetAllMethods(isStatic).Where(m => m.Name == name && !m.IsGenericMethod);
            foreach (var method in methods)
            {
                var methodInfo = CheckMethodInfoParameters(parametersTypes, method);
                if (methodInfo != null)
                {
                    return methodInfo;
                }
            }

            return null;
        }

        private static MethodInfo CheckMethodInfoParameters(Type[] parametersTypes, MethodInfo method)
        {
            var parameters = method.GetParameters();
            //TODO: test if instance method with the same number parameters but different types will not break this code
            var parametersTypesValid = parameters.Length == parametersTypes.Length;
            if (!parametersTypesValid)
            {
                return null;
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
                return method;
            }

            return null;
        }

        public static MethodInfo GetMethodInfo(this Type source, string name, Type[] parametersTypes,
            Type[] typeParameters = null, bool isStatic = false)
        {
            MethodInfo methodInfo = null;
            if (typeParameters == null || typeParameters.Length == 0)
            {
#if NotSupports_GetMethod_WithNameBindingFlagsAndTypes
                var enumerateMethods = false;
                try
                {
                    methodInfo = source.GetNonGenericMethod(name, parametersTypes, isStatic);
                }
                catch (AmbiguousMatchException)
                {
                    //if more than one method it means there are more than one method with name and parameters types i.e. generics
                    enumerateMethods = true;
                }

#if Supports_GetMethod_OnlyWithNameAndBindingFlags
                if (methodInfo == null)
                {
                    enumerateMethods = true;
                }
#endif

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


#if NotSupports_GetMethod_WithNameBindingFlagsAndTypes
        public static MethodInfo GetNonGenericMethod(this Type source, string name, Type[] parametersTypes,
            bool isStatic)
        {
            var staticOrInstance = isStatic ? BindingFlags.Static : BindingFlags.Instance;
            var typeInfo = source.GetTypeInfo();
#if !(NETCORE)
            var methodInfo = (typeInfo.GetMethod(name, staticOrInstance | BindingFlags.Public, null, parametersTypes, null) ??
                     typeInfo.GetMethod(name, staticOrInstance | PrivateOrProtectedBindingFlags, null, parametersTypes, null)) ??
                     typeInfo.GetMethod(name, staticOrInstance | InternalBindingFlags, null, parametersTypes, null); 
#endif
#if NETCOREAPP2_0
            var methodInfo = typeInfo.GetMethod(name, staticOrInstance, null, CallingConventions.Any, parametersTypes, null)??
                typeInfo.GetMethod(name, staticOrInstance | PrivateOrProtectedBindingFlags, null, CallingConventions.Any, parametersTypes, null)??
                typeInfo.GetMethod(name, staticOrInstance | InternalBindingFlags, null, CallingConventions.Any, parametersTypes, null);
#elif Supports_GetMethod_OnlyWithNameAndBindingFlags
            var methodInfo = typeInfo.GetMethod(name, staticOrInstance | BindingFlags.Public) ??
                typeInfo.GetMethod(name, staticOrInstance | PrivateOrProtectedBindingFlags) ??
                typeInfo.GetMethod(name, staticOrInstance | InternalBindingFlags);
#endif
            //TODO: test if necessary
            if (methodInfo != null && methodInfo.IsGenericMethod)
            {
                methodInfo = null;
            }
#if Supports_GetMethod_OnlyWithNameAndBindingFlags
            if (methodInfo != null)
            {
                methodInfo = CheckMethodInfoParameters(parametersTypes, methodInfo);
            }
#endif
            return methodInfo;
        }
#endif

#if !NETSTANDARD1_1
        public static
#if Supports_EventAndProperty_AccessorsViaMethods
            CPropertyInfo
#else
            PropertyInfo
#endif
            GetPropertyInfo(this Type source, string name, bool isStatic)
        {
            var staticOrInstance = isStatic ? BindingFlags.Static : BindingFlags.Instance;
            var typeInfo = source.GetTypeInfo();
            var propertyInfo = typeInfo.GetProperty(name, staticOrInstance) ??
                typeInfo.GetProperty(name, staticOrInstance | PrivateOrProtectedBindingFlags) ??
                typeInfo.GetProperty(name, staticOrInstance | InternalBindingFlags);
#if Supports_EventAndProperty_AccessorsViaMethods
            return new CPropertyInfo(propertyInfo);
#else
            return propertyInfo;
#endif
        }
#endif

        public static FieldInfo GetFieldInfo(this Type source, string fieldName, bool isStatic)
        {
#if NETSTANDARD1_1
            return source.GetTypeInfo().GetDeclaredField(fieldName);
#else
            var staticOrInstance = isStatic ? BindingFlags.Static : BindingFlags.Instance;
            var typeInfo = source.GetTypeInfo();
            var fieldInfo = (typeInfo.GetField(fieldName, staticOrInstance) ??
                    typeInfo.GetField(fieldName, staticOrInstance | PrivateOrProtectedBindingFlags)) ??
                typeInfo.GetField(fieldName, staticOrInstance | InternalBindingFlags);
            return fieldInfo;
#endif
        }

        private static IEnumerable<PropertyInfo> GetAllProperties(this Type source)
        {
#if NETSTANDARD1_1 || NETSTANDARD1_5
            return source.GetRuntimeProperties();
#else
            var properties = source.GetProperties().Concat(
                    source.GetProperties(BindingFlags.NonPublic)).Concat(
                    source.GetProperties(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance))
                .ToArray();
            return properties;
#endif
        }

        public static
#if Supports_EventAndProperty_AccessorsViaMethods
            CPropertyInfo
#else
            PropertyInfo
#endif
            GetIndexerPropertyInfo(this Type source, Type[] indexesTypes,
                string indexerName = null)
        {
            indexerName = indexerName ?? Item;
            var properties = source.GetAllProperties().ToArray();
            if (indexerName == Item)
            {
                var firstIndexerInfo = properties.FirstOrDefault(p => p.GetIndexParameters().Length > 0);
                if (firstIndexerInfo != null && firstIndexerInfo.Name != indexerName)
                {
                    indexerName = firstIndexerInfo.Name;
                }
            }

            var indexerInfo = properties.FirstOrDefault(p => p.Name == indexerName
                && IndexParametersEquals(p.GetIndexParameters(), indexesTypes));
            if (indexerInfo != null)
            {
#if Supports_EventAndProperty_AccessorsViaMethods
                return new CPropertyInfo(indexerInfo);
#else
                return indexerInfo;
#endif
            }

            return null;
        }

#if NotSupports_GetConstructor_ByBindingFlagsAndParameterTypes
        private static IEnumerable<ConstructorInfo> GetAllConstructors(this Type source)
        {
#if NETSTANDARD1_1
            return source.GetTypeInfo().DeclaredConstructors.Where(c => !c.IsStatic);
#else
            var constructors = source.GetTypeInfo().GetConstructors(BindingFlags.Public).Concat(
                source.GetTypeInfo().GetConstructors(PrivateOrProtectedBindingFlags)).Concat(
                source.GetTypeInfo().GetConstructors(InternalBindingFlags | BindingFlags.Instance));
            return constructors;
#endif
        }
#endif

        public static ConstructorInfo GetConstructorInfo(this Type source, Type[] types)
        {
#if NotSupports_GetConstructor_ByBindingFlagsAndParameterTypes
            ConstructorInfo constructor = null;
            var constructors = source.GetAllConstructors();
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
                        source.GetConstructor(InternalBindingFlags | BindingFlags.Instance, null, types, null);
#endif
        }

        private static EventInfo GetEventByName(this Type source, string eventName)
        {
#if NETSTANDARD1_1
            return source.GetTypeInfo().GetDeclaredEvent(eventName);
#else
            return (source.GetTypeInfo().GetEvent(eventName)
                    ?? source.GetTypeInfo().GetEvent(eventName, PrivateOrProtectedBindingFlags))
                ?? source.GetTypeInfo().GetEvent(eventName,
                    InternalBindingFlags | BindingFlags.Instance);
#endif
        }

        public static
#if Supports_EventAndProperty_AccessorsViaMethods
            CEventInfo
#else
            EventInfo
#endif
            GetEventInfo(this Type sourceType, string eventName)
        {
            var eventInfo = sourceType.GetEventByName(eventName);
#if Supports_EventAndProperty_AccessorsViaMethods
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