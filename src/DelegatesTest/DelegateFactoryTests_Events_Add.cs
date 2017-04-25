using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using Delegates;
using DelegatesTest.TestObjects;
#if NETCORE||STANDARD
using Assert = DelegatesTest.CAssert;
using TestMethodAttribute = Xunit.FactAttribute;
#else
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
#endif

namespace DelegatesTest
{
#if !(NETCORE||STANDARD)
    [TestClass]
#endif
    public class DelegateFactoryTests_Events_Add
    {
        private const string TestValue = "test";
        private readonly TestClass _testClassInstance = new TestClass();
        private readonly Type _testClassType = typeof(TestClass);
        private readonly Type _testStructType = typeof(TestStruct);
        private TestStruct _testStructInstance = new TestStruct(0);
        private readonly Type _interfaceType = typeof(IService);
        private readonly IService _interfaceImpl = new Service();

        [TestMethod]
        public void EventAdd_ByTypes_Public()
        {
            var accessor = DelegateFactory.EventAdd<TestClass, TestClass.PublicEventArgs>("PublicEvent");
            Assert.IsNotNull(accessor);
            var eventExecuted = false;
            accessor(_testClassInstance, (sender, args) =>
            {
                eventExecuted = true;
                Assert.IsInstanceOfType(args, typeof(TestClass.PublicEventArgs));
            });
            _testClassInstance.InvokePublicEvent();
            Assert.IsTrue(eventExecuted);
        }

        [TestMethod]
        public void EventAdd_ByTypes_Internal()
        {
            var accessor = DelegateFactory.EventAdd<TestClass, TestClass.InternalEventArgs>("InternalEvent");
            Assert.IsNotNull(accessor);
            var eventExecuted = false;
            accessor(_testClassInstance, (sender, args) =>
            {
                eventExecuted = true;
                Assert.IsInstanceOfType(args, typeof(TestClass.InternalEventArgs));
            });
            _testClassInstance.InvokeInternalEvent();
            Assert.IsTrue(eventExecuted);
        }

        [TestMethod]
        public void EventAdd_ByTypes_Protected()
        {
            var accessor = DelegateFactory.EventAdd<TestClass, TestClass.ProtectedEventArgs>("ProtectedEvent");
            Assert.IsNotNull(accessor);
            var eventExecuted = false;
            accessor(_testClassInstance, (sender, args) =>
            {
                eventExecuted = true;
                Assert.IsInstanceOfType(args, typeof(TestClass.ProtectedEventArgs));
            });
            _testClassInstance.InvokeProtectedEvent();
            Assert.IsTrue(eventExecuted);
        }

        [TestMethod]
        public void EventAdd_ByTypes_Private()
        {
            var accessor = DelegateFactory.EventAdd<TestClass, TestClass.PrivateEventArgs>("PrivateEvent");
            Assert.IsNotNull(accessor);
            var eventExecuted = false;
            accessor(_testClassInstance, (sender, args) =>
            {
                eventExecuted = true;
                Assert.IsInstanceOfType(args, typeof(TestClass.PrivateEventArgs));
            });
            _testClassInstance.InvokePrivateEvent();
            Assert.IsTrue(eventExecuted);
        }

        [TestMethod]
        public void EventAdd_ByTypes_NotExistingEvent()
        {
            var accessor = DelegateFactory.EventAdd<TestStruct, TestClass.PrivateEventArgs>("PrivateEvent");
            Assert.IsNull(accessor);
        }

        [TestMethod]
        public void EventAdd_ByTypes_Incorrect_EventType()
        {
            AssertHelper.ThrowsException<ArgumentException>(() =>
            {
                DelegateFactory.EventAdd<TestClass, TestClass.PublicEventArgs>("PrivateEvent");
            });
        }

        [TestMethod]
        public void EventAdd_ByObjectAndType_Public()
        {
            var accessor = _testClassType.EventAdd<TestClass.PublicEventArgs>("PublicEvent");
            Assert.IsNotNull(accessor);
            var eventExecuted = false;
            accessor(_testClassInstance, (sender, args) =>
            {
                eventExecuted = true;
                Assert.IsInstanceOfType(args, typeof(TestClass.PublicEventArgs));
            });
            _testClassInstance.InvokePublicEvent();
            Assert.IsTrue(eventExecuted);
        }

        [TestMethod]
        public void EventAdd_ByObjectAndType_Internal()
        {
            var accessor = _testClassType.EventAdd<TestClass.InternalEventArgs>("InternalEvent");
            Assert.IsNotNull(accessor);
            var eventExecuted = false;
            accessor(_testClassInstance, (sender, args) =>
            {
                eventExecuted = true;
                Assert.IsInstanceOfType(args, typeof(TestClass.InternalEventArgs));
            });
            _testClassInstance.InvokeInternalEvent();
            Assert.IsTrue(eventExecuted);
        }

        [TestMethod]
        public void EventAdd_ByObjectAndType_Protected()
        {
            var accessor = _testClassType.EventAdd<TestClass.ProtectedEventArgs>("ProtectedEvent");
            Assert.IsNotNull(accessor);
            var eventExecuted = false;
            accessor(_testClassInstance, (sender, args) =>
            {
                eventExecuted = true;
                Assert.IsInstanceOfType(args, typeof(TestClass.ProtectedEventArgs));
            });
            _testClassInstance.InvokeProtectedEvent();
            Assert.IsTrue(eventExecuted);
        }

        [TestMethod]
        public void EventAdd_ByObjectAndType_Private()
        {
            var accessor = _testClassType.EventAdd<TestClass.PrivateEventArgs>("PrivateEvent");
            Assert.IsNotNull(accessor);
            var eventExecuted = false;
            accessor(_testClassInstance, (sender, args) =>
            {
                eventExecuted = true;
                Assert.IsInstanceOfType(args, typeof(TestClass.PrivateEventArgs));
            });
            _testClassInstance.InvokePrivateEvent();
            Assert.IsTrue(eventExecuted);
        }

        [TestMethod]
        public void EventAdd_ByObjectAndType_NotExistingEvent()
        {
            var accessor = _testStructType.EventAdd<TestClass.PrivateEventArgs>("PrivateEvent");
            Assert.IsNull(accessor);
        }

        [TestMethod]
        public void EventAdd_ByObjectAndType_Incorrect_EventType()
        {
            AssertHelper.ThrowsException<ArgumentException>(() =>
            {
                _testClassType.EventAdd<TestClass.PublicEventArgs>("PrivateEvent");
            });
        }

        [TestMethod]
        public void EventAdd_ByObjects_Public()
        {
            var accessor = _testClassType.EventAdd("PublicEvent");
            Assert.IsNotNull(accessor);
            var eventExecuted = false;
            accessor(_testClassInstance, (sender, args) =>
            {
                eventExecuted = true;
                Assert.IsInstanceOfType(args, typeof(TestClass.PublicEventArgs));
            });
            _testClassInstance.InvokePublicEvent();
            Assert.IsTrue(eventExecuted);
        }

        [TestMethod]
        public void EventAdd_ByObjects_Internal()
        {
            var accessor = _testClassType.EventAdd("InternalEvent");
            Assert.IsNotNull(accessor);
            var eventExecuted = false;
            accessor(_testClassInstance, (sender, args) =>
            {
                eventExecuted = true;
                Assert.IsInstanceOfType(args, typeof(TestClass.InternalEventArgs));
            });
            _testClassInstance.InvokeInternalEvent();
            Assert.IsTrue(eventExecuted);
        }

        [TestMethod]
        public void EventAdd_ByObjects_Protected()
        {
            var accessor = _testClassType.EventAdd("ProtectedEvent");
            Assert.IsNotNull(accessor);
            var eventExecuted = false;
            accessor(_testClassInstance, (sender, args) =>
            {
                eventExecuted = true;
                Assert.IsInstanceOfType(args, typeof(TestClass.ProtectedEventArgs));
            });
            _testClassInstance.InvokeProtectedEvent();
            Assert.IsTrue(eventExecuted);
        }

        [TestMethod]
        public void EventAdd_ByObjects_Private()
        {
            var accessor = _testClassType.EventAdd("PrivateEvent");
            Assert.IsNotNull(accessor);
            var eventExecuted = false;
            accessor(_testClassInstance, (sender, args) =>
            {
                eventExecuted = true;
                Assert.IsInstanceOfType(args, typeof(TestClass.PrivateEventArgs));
            });
            _testClassInstance.InvokePrivateEvent();
            Assert.IsTrue(eventExecuted);
        }

        [TestMethod]
        public void EventAdd_ByObjects_NotExistingEvent()
        {
            var accessor = _testStructType.EventAdd("PrivateEvent");
            Assert.IsNull(accessor);
        }

        [TestMethod]
        public void EventAdd_Interface_ByObjects()
        {
            var accessor = _interfaceType.EventAdd("Event");
            Assert.IsNotNull(accessor);
            var eventExecuted = false;
            Action<object, object> eventAction = (sender, args) =>
            {
                eventExecuted = true;
                Assert.IsInstanceOfType(args, typeof(EventArgs));
            };
            accessor(_interfaceImpl, eventAction);
            _interfaceImpl.InvokeEvent();
            Assert.IsTrue(eventExecuted);
        }

        [TestMethod]
        public void EventAdd_Interface_ByObjectAndType()
        {
            var accessor = _interfaceType.EventAdd<EventArgs>("Event");
            Assert.IsNotNull(accessor);
            var eventExecuted = false;
            EventHandler<EventArgs> eventAction = (sender, args) =>
            {
                eventExecuted = true;
                Assert.IsInstanceOfType(args, typeof(EventArgs));
            };
            accessor(_interfaceImpl, eventAction);
            _interfaceImpl.InvokeEvent();
            Assert.IsTrue(eventExecuted);
        }

        [TestMethod]
        public void EventAdd_Interface_ByTypes()
        {
            var accessor = DelegateFactory.EventAdd<IService, EventArgs>("Event");
            Assert.IsNotNull(accessor);
            var eventExecuted = false;
            EventHandler<EventArgs> eventAction = (sender, args) =>
            {
                eventExecuted = true;
                Assert.IsInstanceOfType(args, typeof(EventArgs));
            };
            accessor(_interfaceImpl, eventAction);
            _interfaceImpl.InvokeEvent();
            Assert.IsTrue(eventExecuted);
        }

        [TestMethod]
        public void EventAdd_CustomDelegate_ByObjects()
        {
            var accessor = _testClassType.EventAdd("CusDelegEvent");
            Assert.IsNotNull(accessor);
            var eventExecuted = false;
            Action<object, object> eventAction = (sender, args) =>
            {
                eventExecuted = true;
                Assert.IsInstanceOfType(args, typeof(EventArgs));
            };
            accessor(_testClassInstance, eventAction);
            _testClassInstance.InvokeCusDelegEvent();
            Assert.IsTrue(eventExecuted);
        }

        [TestMethod]
        public void EventAdd_CustomDelegate_ByObjectAndType()
        {
            var accessor = _testClassType.EventAdd<EventArgs>("CusDelegEvent");
            Assert.IsNotNull(accessor);
            var eventExecuted = false;
            EventHandler<EventArgs> eventAction = (sender, args) =>
            {
                eventExecuted = true;
                Assert.IsInstanceOfType(args, typeof(EventArgs));
            };
            accessor(_testClassInstance, eventAction);
            _testClassInstance.InvokeCusDelegEvent();
            Assert.IsTrue(eventExecuted);
        }

        [TestMethod]
        public void EventAdd_CustomDelegate_ByTypeAndObject()
        {
            var accessor = DelegateFactory.EventAdd<TestClass>("CusDelegEvent");
            Assert.IsNotNull(accessor);
            var eventExecuted = false;
            Action<TestClass, object> eventAction = (sender, args) =>
            {
                eventExecuted = true;
                Assert.IsInstanceOfType(args, typeof(EventArgs));
            };
            accessor(_testClassInstance, eventAction);
            _testClassInstance.InvokeCusDelegEvent();
            Assert.IsTrue(eventExecuted);
        }

        [TestMethod]
        public void EventAdd_CustomDelegate_ByTypesAndEventHandler()
        {
            var accessor = DelegateFactory.EventAdd<TestClass, EventArgs>("CusDelegEvent");
            Assert.IsNotNull(accessor);
            var eventExecuted = false;
            EventHandler<EventArgs> eventAction = (sender, args) =>
            {
                eventExecuted = true;
                Assert.IsInstanceOfType(args, typeof(EventArgs));
            };
            accessor(_testClassInstance, eventAction);
            _testClassInstance.InvokeCusDelegEvent();
            Assert.IsTrue(eventExecuted);
        }

        [TestMethod]
        public void EventAdd_CustomDelegate_ByTypes()
        {
            var accessor = DelegateFactory.EventAddCustomDelegate<TestClass, EventCustomDelegate>("CusDelegEvent");
            Assert.IsNotNull(accessor);
            var eventExecuted = false;
            EventCustomDelegate eventAction = (sender, args) =>
            {
                eventExecuted = true;
                Assert.IsInstanceOfType(args, typeof(EventArgs));
            };
            accessor(_testClassInstance, eventAction);
            _testClassInstance.InvokeCusDelegEvent();
            Assert.IsTrue(eventExecuted);
        }

        [TestMethod]
        public void EventAdd_CustomDelegate_IncompatibleDelegate()
        {
            ArgumentException ae = null;
            try
            {
                DelegateFactory.EventAddCustomDelegate<TestClass, EventHandler<ConsoleCancelEventArgs>>("CusDelegEvent");
            }
            catch (ArgumentException e)
            {
                ae = e;
            }
            Assert.IsNotNull(ae);
            Assert.IsInstanceOfType(ae, typeof(ArgumentException));
        }
    }
}
