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
    public class CPropertyInfo
    {
        private readonly PropertyInfo _property;

        public CPropertyInfo(PropertyInfo property)
        {
            _property = property;
        }

        public MethodInfo GetMethod
        {
            get
            {
                return _property?.GetAccessors().Concat(_property.GetAccessors(true))
                    .FirstOrDefault(a => a.Name.StartsWith("get"));
            }
        }

        public MethodInfo SetMethod
        {
            get
            {
                return _property?.GetAccessors().Concat(_property.GetAccessors(true))
                    .FirstOrDefault(a => a.Name.StartsWith("set"));
            }
        }

        public Type PropertyType => _property?.PropertyType;
    }
}