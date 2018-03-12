// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DelegateFactory_EventInvoke.cs" company="Natan Podbielski">
//   Copyright (c) 2016 - 2018 Natan Podbielski. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Linq.Expressions;
using System.Reflection;
using Delegates.Extensions;
using Delegates.Helper;

namespace Delegates
{
    public static partial class DelegateFactory
    {
        /// <summary>
        /// Creates delegates for invoking event with name <paramref name="eventName"/> on type
        /// <typeparamref name="TSource" /> with event args type <typeparamref name="TEventArgs"/>
        /// </summary>
        /// <typeparam name="TSource">Type with event</typeparam>
        /// <typeparam name="TEventArgs">Event args type</typeparam>
        /// <param name="eventName">Name of event</param>
        /// <returns></returns>
        public static Action<TSource, TEventArgs> EventInvoke<TSource, TEventArgs>(string eventName)
        {
            var sourceType = typeof(TSource);
            return EventInvokeImpl<Action<TSource, TEventArgs>>(sourceType, eventName, typeof(TSource),
                typeof(TEventArgs));
        }

        /// <summary>
        /// Creates delegates for invoking event with name <paramref name="eventName"/> on type
        /// <paramref name="source"/> with event args type <typeparamref name="TEventArgs"/>
        /// </summary>
        /// <typeparam name="TEventArgs">Event args type</typeparam>
        /// <param name="source">Type with event</param>
        /// <param name="eventName">Name of event</param>
        /// <returns></returns>
        public static Action<object, TEventArgs> EventInvoke<TEventArgs>(this Type source, string eventName)
        {
            return EventInvokeImpl<Action<object, TEventArgs>>(source, eventName, typeof(object), typeof(TEventArgs));
        }

        /// <summary>
        /// Creates delegates for invoking event with name <paramref name="eventName"/> on type
        /// <paramref name="source"/> with object as an event args
        /// </summary>
        /// <param name="source">Type with event</param>
        /// <param name="eventName">Name of event</param>
        /// <returns></returns>
        public static Action<object, object> EventInvoke(this Type source, string eventName)
        {
            return EventInvokeImpl<Action<object, object>>(source, eventName, typeof(object), typeof(object));
        }

        /// <summary>
        /// Creates delegates for invoking event with name <paramref name="eventName"/> on type
        /// <typeparamref name="TSource"/> using custom delegate type <typeparamref name="TDelegate"/>
        /// </summary>
        /// <typeparam name="TSource">Type with event</typeparam>
        /// <typeparam name="TDelegate">Event custom delegate type</typeparam>
        /// <param name="eventName">Name of event</param>
        /// <returns></returns>
        public static TDelegate EventInvokeCustomDelegate<TSource, TDelegate>(string eventName)
            where TDelegate : class
        {
            return EventInvokeImpl<TDelegate>(typeof(TSource), eventName);
        }

        /// <summary> 
        /// Creates delegates for invoking event with name <paramref name="eventName"/> on type
        /// <paramref name="source"/> using custom delegate type <typeparamref name="TDelegate"/>
        /// </summary>
        /// <typeparam name="TDelegate">Event custom delegate type</typeparam>
        /// <param name="source">Type with event</param>
        /// <param name="eventName">Name of event</param>
        /// <returns></returns>
        public static TDelegate EventInvokeCustomDelegate<TDelegate>(this Type source, string eventName)
            where TDelegate : class
        {
            return EventInvokeImpl<TDelegate>(source, eventName);
        }

        private static TDelegate EventInvokeImpl<TDelegate>(Type source, string eventName, Type lambdaSender = null,
            Type lambdaArgs = null)
            where TDelegate : class
        {
            var fieldInfo = source.GetFieldInfo(eventName, false);
            if (fieldInfo != null)
            {
                var sender = fieldInfo.FieldType.GetDelegateFirstParameter();
                var eventArgs = fieldInfo.FieldType.GetDelegateSecondParameter();
                var lambaSenderParam = Expression.Parameter(lambdaSender ?? sender, "source");
                if (lambdaArgs != null)
                    DelegateHelper.IsEventArgsTypeCorrect(eventArgs, lambdaArgs, true);
                else
                    lambdaArgs = eventArgs;
                var lambdaArgsParam = Expression.Parameter(lambdaArgs, "args");
                var eventArgsParam = Expression.Convert(lambdaArgsParam, eventArgs);
                var invokeMethod = fieldInfo.FieldType.GetMethod("Invoke");
                var fieldExpression = Expression.Field(Expression.Convert(lambaSenderParam, source), fieldInfo);
                var conditionExpression = Expression.IsTrue(
                    Expression.NotEqual(fieldExpression, Expression.Constant(null)));
                var callSenderExpr = lambdaSender != sender
                    ? Expression.Convert(lambaSenderParam, sender)
                    : (Expression)lambaSenderParam;
                var callExpression = Expression.Call(fieldExpression, invokeMethod, callSenderExpr, eventArgsParam);
                var ifExpress = Expression.IfThen(conditionExpression, callExpression);
                var lambdaExpression = Expression.Lambda<TDelegate>(ifExpress, lambaSenderParam, lambdaArgsParam);
                return lambdaExpression.Compile();
            }

            return null;
        }
    }

    //internal class ChainDelegate
    //{
    //    private List<Func<object, object>> _chain;

    //    public void Add(Func<object, object> deleg)
    //    {
    //        if (_chain == null)
    //        {
    //            _chain = new List<Func<object, object>>();
    //        }
    //        _chain.Add(deleg);
    //    }
    //}
}