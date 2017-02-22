// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CPropertyInfo.cs" company="Natan Podbielski">
//   Copyright (c) 2016 - 2016 Natan Podbielski. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Reflection;

namespace Delegates.Extensions
{
    /// <summary>
    /// Compatibility class (wrapper) if version of .NET do not support v4.5 of <see cref="EventInfo"/> class.
    /// </summary>
    public class CPropertyInfo
    {
        private readonly PropertyInfo _property;

        /// <summary>
        /// Wrapper constructor
        /// </summary>
        /// <param name="property">Incompatible <see cref="PropertyInfo"/> class instance</param>
        public CPropertyInfo(PropertyInfo property)
        {
            _property = property;
        }

        /// <summary>
        /// Property get accessor
        /// </summary>
        public MethodInfo GetMethod
        {
            get
            {
                return _property?.GetAccessors().Concat(_property.GetAccessors(true))
                    .FirstOrDefault(a => a.Name.StartsWith("get"));
            }
        }

        /// <summary>
        /// Proxy property of <see cref="PropertyInfo.PropertyType"/>
        /// </summary>
        public Type PropertyType => _property?.PropertyType;

        /// <summary>
        /// Property set accessor
        /// </summary>
        public MethodInfo SetMethod
        {
            get
            {
                return _property?.GetAccessors().Concat(_property.GetAccessors(true))
                    .FirstOrDefault(a => a.Name.StartsWith("set"));
            }
        }
    }
}