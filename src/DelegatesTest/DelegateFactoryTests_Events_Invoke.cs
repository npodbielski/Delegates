using System;
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

#if !NET35
#if !(NETCORE || STANDARD)
    [TestClass]
#endif
    public class DelegateFactoryTests_Events_Invoke
    {
        private const string TestValue = "test";
        private readonly TestClass _testClassInstance = new TestClass();
        private readonly Type _testClassType = typeof(TestClass);
        private readonly Type _testStructType = typeof(TestStruct);
        private TestStruct _testStructInstance = new TestStruct(0);
        private readonly Type _interfaceType = typeof(IService);
        private readonly IService _interfaceImpl = new Service();

        [TestMethod]
        public void EventCall_ByTypes_Public()
        {
            var call = DelegateFactory.EventInvoke<TestClass, TestClass.PublicEventArgs>("PublicEvent");
            Assert.IsNotNull(call);
            var eventExecuted = false;
            EventHandler<TestClass.PublicEventArgs> eventHandler = (sender, args) =>
            {
                eventExecuted = true;
                Assert.IsInstanceOfType(args, typeof(TestClass.PublicEventArgs));
            };
            _testClassInstance.AddPublicEventHandler(eventHandler);
            call(_testClassInstance, new TestClass.PublicEventArgs());
            Assert.IsTrue(eventExecuted);
        }

        [TestMethod]
        public void EventCall_ByTypes_Internal()
        {
            var call = DelegateFactory.EventInvoke<TestClass, TestClass.InternalEventArgs>("InternalEventBackend");
            Assert.IsNotNull(call);
            var eventExecuted = false;
            EventHandler<TestClass.InternalEventArgs> eventHandler = (sender, args) =>
            {
                eventExecuted = true;
                Assert.IsInstanceOfType(args, typeof(TestClass.InternalEventArgs));
            };
            _testClassInstance.AddInternalEventHandler(eventHandler);
            call(_testClassInstance, new TestClass.InternalEventArgs());
            Assert.IsTrue(eventExecuted);
        }

        [TestMethod]
        public void EventCall_ByTypes_Protected()
        {
            var call = DelegateFactory.EventInvoke<TestClass, TestClass.ProtectedEventArgs>("ProtectedEvent");
            Assert.IsNotNull(call);
            var eventExecuted = false;
            EventHandler<TestClass.ProtectedEventArgs> eventHandler = (sender, args) =>
            {
                eventExecuted = true;
                Assert.IsInstanceOfType(args, typeof(TestClass.ProtectedEventArgs));
            };
            _testClassInstance.AddProtectedEventHandler(eventHandler);
            call(_testClassInstance, new TestClass.ProtectedEventArgs());
            Assert.IsTrue(eventExecuted);
        }

        [TestMethod]
        public void EventCall_ByTypes_Private()
        {
            var call = DelegateFactory.EventInvoke<TestClass, TestClass.PrivateEventArgs>("PrivateEvent");
            Assert.IsNotNull(call);
            var eventExecuted = false;
            EventHandler<TestClass.PrivateEventArgs> eventHandler = (sender, args) =>
            {
                eventExecuted = true;
                Assert.IsInstanceOfType(args, typeof(TestClass.PrivateEventArgs));
            };
            _testClassInstance.AddPrivateEventHandler(eventHandler);
            call(_testClassInstance, new TestClass.PrivateEventArgs());
            Assert.IsTrue(eventExecuted);
        }

        [TestMethod]
        public void EventCall_ByTypes_EventArgsAsObject()
        {
            var call = DelegateFactory.EventInvoke<TestClass, object>("PublicEvent");
            Assert.IsNotNull(call);
            var eventExecuted = false;
            EventHandler<TestClass.PublicEventArgs> eventHandler = (sender, args) =>
            {
                eventExecuted = true;
                Assert.IsInstanceOfType(args, typeof(TestClass.PublicEventArgs));
            };
            _testClassInstance.AddPublicEventHandler(eventHandler);
            call(_testClassInstance, new TestClass.PublicEventArgs());
            Assert.IsTrue(eventExecuted);
        }

        [TestMethod]
        public void EventCall_ByTypes_NoEvent()
        {
            var call = DelegateFactory.EventInvoke<TestStruct, TestClass.PublicEventArgs>("PublicEvent");
            Assert.IsNull(call);
        }

        [TestMethod]
        public void EventCall_ByTypes_WrongName()
        {
            var call = DelegateFactory.EventInvoke<TestClass, TestClass.PublicEventArgs>("WrongName");
            Assert.IsNull(call);
        }

        [TestMethod]
        public void EventCall_ByTypes_IncorrectEventType()
        {
            AssertHelper.ThrowsException<ArgumentException>(() =>
            {
                DelegateFactory.EventInvoke<TestClass, TestClass.PrivateEventArgs>("PublicEvent");
            });
        }

        /// <summary>
        /// Tests null checking in EventInvoke delegate (cant call event if there is not handlers attached)
        /// </summary>
        [TestMethod]
        public void EventCall_ByTypes_Public_EventIsNull()
        {
            var call = DelegateFactory.EventInvoke<TestClass, TestClass.PublicEventArgs>("PublicEvent");
            Assert.IsNotNull(call);
            call(_testClassInstance, new TestClass.PublicEventArgs());
        }

        [TestMethod]
        public void EventCall_ByTypes_Internal_EventIsNull()
        {
            var call = DelegateFactory.EventInvoke<TestClass, TestClass.InternalEventArgs>("InternalEventBackend");
            Assert.IsNotNull(call);
            call(_testClassInstance, new TestClass.InternalEventArgs());
        }

        [TestMethod]
        public void EventCall_ByTypes_Protected_EventIsNull()
        {
            var call = DelegateFactory.EventInvoke<TestClass, TestClass.ProtectedEventArgs>("ProtectedEvent");
            Assert.IsNotNull(call);
            call(_testClassInstance, new TestClass.ProtectedEventArgs());
        }

        [TestMethod]
        public void EventCall_ByTypes_Private_EventIsNull()
        {
            var call = DelegateFactory.EventInvoke<TestClass, TestClass.PrivateEventArgs>("PrivateEvent");
            Assert.IsNotNull(call);
            call(_testClassInstance, new TestClass.PrivateEventArgs());
        }

        [TestMethod]
        public void EventCall_ByObjectAndType_Public()
        {
            var call = _testClassType.EventInvoke<TestClass.PublicEventArgs>("PublicEvent");
            Assert.IsNotNull(call);
            var eventExecuted = false;
            EventHandler<TestClass.PublicEventArgs> eventHandler = (sender, args) =>
            {
                eventExecuted = true;
                Assert.IsInstanceOfType(args, typeof(TestClass.PublicEventArgs));
            };
            _testClassInstance.AddPublicEventHandler(eventHandler);
            call(_testClassInstance, new TestClass.PublicEventArgs());
            Assert.IsTrue(eventExecuted);
        }

        [TestMethod]
        public void EventCall_ByObjectAndType_Internal()
        {
            var call = _testClassType.EventInvoke<TestClass.InternalEventArgs>("InternalEventBackend");
            Assert.IsNotNull(call);
            var eventExecuted = false;
            EventHandler<TestClass.InternalEventArgs> eventHandler = (sender, args) =>
            {
                eventExecuted = true;
                Assert.IsInstanceOfType(args, typeof(TestClass.InternalEventArgs));
            };
            _testClassInstance.AddInternalEventHandler(eventHandler);
            call(_testClassInstance, new TestClass.InternalEventArgs());
            Assert.IsTrue(eventExecuted);
        }

        [TestMethod]
        public void EventCall_ByObjectAndType_Protected()
        {
            var call = _testClassType.EventInvoke<TestClass.ProtectedEventArgs>("ProtectedEvent");
            Assert.IsNotNull(call);
            var eventExecuted = false;
            EventHandler<TestClass.ProtectedEventArgs> eventHandler = (sender, args) =>
            {
                eventExecuted = true;
                Assert.IsInstanceOfType(args, typeof(TestClass.ProtectedEventArgs));
            };
            _testClassInstance.AddProtectedEventHandler(eventHandler);
            call(_testClassInstance, new TestClass.ProtectedEventArgs());
            Assert.IsTrue(eventExecuted);
        }

        [TestMethod]
        public void EventCall_ByObjectAndType_Private()
        {
            var call = _testClassType.EventInvoke<TestClass.PrivateEventArgs>("PrivateEvent");
            Assert.IsNotNull(call);
            var eventExecuted = false;
            EventHandler<TestClass.PrivateEventArgs> eventHandler = (sender, args) =>
            {
                eventExecuted = true;
                Assert.IsInstanceOfType(args, typeof(TestClass.PrivateEventArgs));
            };
            _testClassInstance.AddPrivateEventHandler(eventHandler);
            call(_testClassInstance, new TestClass.PrivateEventArgs());
            Assert.IsTrue(eventExecuted);
        }

        [TestMethod]
        public void EventCall_ByObjectAndType_NoEvent()
        {
            var call = _testStructType.EventInvoke<TestClass.PublicEventArgs>("PublicEvent");
            Assert.IsNull(call);
        }

        [TestMethod]
        public void EventCall_ByObjectAndType_WrongName()
        {
            var call = _testClassType.EventInvoke<TestClass.PublicEventArgs>("WrongName");
            Assert.IsNull(call);
        }

        [TestMethod]
        public void EventCall_ByObjectAndType_IncorrectEventType()
        {
            AssertHelper.ThrowsException<ArgumentException>(() =>
            {
                _testClassType.EventInvoke<TestClass.PrivateEventArgs>("PublicEvent");
            });
        }

        [TestMethod]
        public void EventCall_ByObjectAndType_EventArgsAsObject()
        {
            var call = _testClassType.EventInvoke<object>("PublicEvent");
            Assert.IsNotNull(call);
            var eventExecuted = false;
            EventHandler<TestClass.PublicEventArgs> eventHandler = (sender, args) =>
            {
                eventExecuted = true;
                Assert.IsInstanceOfType(args, typeof(TestClass.PublicEventArgs));
            };
            _testClassInstance.AddPublicEventHandler(eventHandler);
            call(_testClassInstance, new TestClass.PublicEventArgs());
            Assert.IsTrue(eventExecuted);
        }

        /// <summary>
        /// Tests null checking in EventInvoke delegate (cant call event if there is not handlers attached)
        /// </summary>
        [TestMethod]
        public void EventCall_ByObjectAndType_Public_EventIsNull()
        {
            var call = DelegateFactory.EventInvoke<TestClass, TestClass.PublicEventArgs>("PublicEvent");
            Assert.IsNotNull(call);
            call(_testClassInstance, new TestClass.PublicEventArgs());
        }

        [TestMethod]
        public void EventCall_ByObjectAndType_Internal_EventIsNull()
        {
            var call = DelegateFactory.EventInvoke<TestClass, TestClass.InternalEventArgs>("InternalEventBackend");
            Assert.IsNotNull(call);
            call(_testClassInstance, new TestClass.InternalEventArgs());
        }

        [TestMethod]
        public void EventCall_ByObjectAndType_Protected_EventIsNull()
        {
            var call = DelegateFactory.EventInvoke<TestClass, TestClass.ProtectedEventArgs>("ProtectedEvent");
            Assert.IsNotNull(call);
            call(_testClassInstance, new TestClass.ProtectedEventArgs());
        }

        [TestMethod]
        public void EventCall_ByObjectAndType_Private_EventIsNull()
        {
            var call = DelegateFactory.EventInvoke<TestClass, TestClass.PrivateEventArgs>("PrivateEvent");
            Assert.IsNotNull(call);
            call(_testClassInstance, new TestClass.PrivateEventArgs());
        }

        [TestMethod]
        public void EventCall_ByObjects_Public()
        {
            var call = _testClassType.EventInvoke("PublicEvent");
            Assert.IsNotNull(call);
            var eventExecuted = false;
            EventHandler<TestClass.PublicEventArgs> eventHandler = (sender, args) =>
            {
                eventExecuted = true;
                Assert.IsInstanceOfType(args, typeof(TestClass.PublicEventArgs));
            };
            _testClassInstance.AddPublicEventHandler(eventHandler);
            call(_testClassInstance, new TestClass.PublicEventArgs());
            Assert.IsTrue(eventExecuted);
        }

        [TestMethod]
        public void EventCall_ByObjects_Internal()
        {
            var call = _testClassType.EventInvoke("InternalEventBackend");
            Assert.IsNotNull(call);
            var eventExecuted = false;
            EventHandler<TestClass.InternalEventArgs> eventHandler = (sender, args) =>
            {
                eventExecuted = true;
                Assert.IsInstanceOfType(args, typeof(TestClass.InternalEventArgs));
            };
            _testClassInstance.AddInternalEventHandler(eventHandler);
            call(_testClassInstance, new TestClass.InternalEventArgs());
            Assert.IsTrue(eventExecuted);
        }

        [TestMethod]
        public void EventCall_ByObjects_Protected()
        {
            var call = _testClassType.EventInvoke("ProtectedEvent");
            Assert.IsNotNull(call);
            var eventExecuted = false;
            EventHandler<TestClass.ProtectedEventArgs> eventHandler = (sender, args) =>
            {
                eventExecuted = true;
                Assert.IsInstanceOfType(args, typeof(TestClass.ProtectedEventArgs));
            };
            _testClassInstance.AddProtectedEventHandler(eventHandler);
            call(_testClassInstance, new TestClass.ProtectedEventArgs());
            Assert.IsTrue(eventExecuted);
        }

        [TestMethod]
        public void EventCall_ByObjects_Private()
        {
            var call = _testClassType.EventInvoke("PrivateEvent");
            Assert.IsNotNull(call);
            var eventExecuted = false;
            EventHandler<TestClass.PrivateEventArgs> eventHandler = (sender, args) =>
            {
                eventExecuted = true;
                Assert.IsInstanceOfType(args, typeof(TestClass.PrivateEventArgs));
            };
            _testClassInstance.AddPrivateEventHandler(eventHandler);
            call(_testClassInstance, new TestClass.PrivateEventArgs());
            Assert.IsTrue(eventExecuted);
        }

        [TestMethod]
        public void EventCall_ByObjects_NoEvent()
        {
            var call = _testStructType.EventInvoke("PublicEvent");
            Assert.IsNull(call);
        }

        [TestMethod]
        public void EventCall_ByObjects_WrongName()
        {
            var call = _testClassType.EventInvoke("WrongName");
            Assert.IsNull(call);
        }

        [TestMethod]
        public void EventCall_ByObjects_IncorrectEventType()
        {
            AssertHelper.ThrowsException<ArgumentException>(() =>
            {
                _testClassType.EventInvoke<TestClass.PrivateEventArgs>("PublicEvent");
            });
        }

        /// <summary>
        /// Tests null checking in EventInvoke delegate (cant call event if there is not handlers attached)
        /// </summary>
        [TestMethod]
        public void EventCall_ByObjects_Public_EventIsNull()
        {
            var call = DelegateFactory.EventInvoke<TestClass, TestClass.PublicEventArgs>("PublicEvent");
            Assert.IsNotNull(call);
            call(_testClassInstance, new TestClass.PublicEventArgs());
        }

        [TestMethod]
        public void EventCall_ByObjects_Internal_EventIsNull()
        {
            var call = DelegateFactory.EventInvoke<TestClass, TestClass.InternalEventArgs>("InternalEventBackend");
            Assert.IsNotNull(call);
            call(_testClassInstance, new TestClass.InternalEventArgs());
        }

        [TestMethod]
        public void EventCall_ByObjects_Protected_EventIsNull()
        {
            var call = DelegateFactory.EventInvoke<TestClass, TestClass.ProtectedEventArgs>("ProtectedEvent");
            Assert.IsNotNull(call);
            call(_testClassInstance, new TestClass.ProtectedEventArgs());
        }

        [TestMethod]
        public void EventCall_ByObjects_Private_EventIsNull()
        {
            var call = DelegateFactory.EventInvoke<TestClass, TestClass.PrivateEventArgs>("PrivateEvent");
            Assert.IsNotNull(call);
            call(_testClassInstance, new TestClass.PrivateEventArgs());
        }

        [TestMethod]
        public void EventCall_ByTypes_CustomEventDelegate()
        {
            var call = DelegateFactory.EventInvoke<TestClass, EventArgs>("CusDelegEvent");
            Assert.IsNotNull(call);
            var eventExecuted = false;
            EventCustomDelegate eventHandler = (sender, args) =>
            {
                eventExecuted = true;
                Assert.IsInstanceOfType(args, typeof(EventArgs));
            };
            _testClassInstance.CusDelegEvent += eventHandler;
            call(_testClassInstance, new EventArgs());
            Assert.IsTrue(eventExecuted);
        }

        [TestMethod]
        public void EventCall_ByTypeAndCustomDelegate()
        {
            var call = DelegateFactory.EventInvokeCustomDelegate<TestClass, EventCustomDelegate>("CusDelegEvent");
            Assert.IsNotNull(call);
            var eventExecuted = false;
            EventCustomDelegate eventHandler = (sender, args) =>
            {
                eventExecuted = true;
                Assert.IsInstanceOfType(args, typeof(EventArgs));
            };
            _testClassInstance.CusDelegEvent += eventHandler;
            call(_testClassInstance, new EventArgs());
            Assert.IsTrue(eventExecuted);
        }

        [TestMethod]
        public void EventCall_ByTypeAndCustomDelegate_IncorrectEventType()
        {
            AssertHelper.ThrowsException<ArgumentException>(() =>
            {
                DelegateFactory.EventInvokeCustomDelegate<TestClass, EventHandler<TestClass.PrivateEventArgs>>("PublicEvent");
            });
        }

        [TestMethod]
        public void EventCall_ByTypeAndCustomDelegate_CompatibleDelegate()
        {
            var call = DelegateFactory.EventInvokeCustomDelegate<TestClass, EventHandler<EventArgs>>
                ("CusDelegEvent");
            Assert.IsNotNull(call);
            var eventExecuted = false;
            EventCustomDelegate eventHandler = (sender, args) =>
            {
                eventExecuted = true;
                Assert.IsInstanceOfType(args, typeof(EventArgs));
            };
            _testClassInstance.CusDelegEvent += eventHandler;
            call(_testClassInstance, new EventArgs());
            Assert.IsTrue(eventExecuted);
        }

        [TestMethod]
        public void EventCall_ByObjectAndCustomDelegate()
        {
            var call = _testClassType.EventInvokeCustomDelegate<EventCustomDelegate>("CusDelegEvent");
            Assert.IsNotNull(call);
            var eventExecuted = false;
            EventCustomDelegate eventHandler = (sender, args) =>
            {
                eventExecuted = true;
                Assert.IsInstanceOfType(args, typeof(EventArgs));
            };
            _testClassInstance.CusDelegEvent += eventHandler;
            call(_testClassInstance, new EventArgs());
            Assert.IsTrue(eventExecuted);
        }

        [TestMethod]
        public void EventCall_ByObjectAndCustomDelegate_CompatibleDelegate()
        {
            var call = _testClassType.EventInvokeCustomDelegate<EventHandler<EventArgs>>("CusDelegEvent");
            Assert.IsNotNull(call);
            var eventExecuted = false;
            EventCustomDelegate eventHandler = (sender, args) =>
            {
                eventExecuted = true;
                Assert.IsInstanceOfType(args, typeof(EventArgs));
            };
            _testClassInstance.CusDelegEvent += eventHandler;
            call(_testClassInstance, new EventArgs());
            Assert.IsTrue(eventExecuted);
        }

        [TestMethod]
        public void EventCall_CustomDelegate_ByTypes()
        {
            var call = DelegateFactory.EventInvoke<TestClass, EventArgs>("CusDelegEvent");
            Assert.IsNotNull(call);
            var eventExecuted = false;
            EventCustomDelegate eventHandler = (sender, args) =>
            {
                eventExecuted = true;
                Assert.IsInstanceOfType(args, typeof(EventArgs));
            };
            _testClassInstance.CusDelegEvent += eventHandler;
            call(_testClassInstance, new EventArgs());
            Assert.IsTrue(eventExecuted);
        }

        [TestMethod]
        public void EventCall_CustomDelegate_ByTypes_EventArgsAsObject()
        {
            var call = DelegateFactory.EventInvoke<TestClass, object>("CusDelegEvent");
            Assert.IsNotNull(call);
            var eventExecuted = false;
            EventCustomDelegate eventHandler = (sender, args) =>
            {
                eventExecuted = true;
                Assert.IsInstanceOfType(args, typeof(EventArgs));
            };
            _testClassInstance.CusDelegEvent += eventHandler;
            call(_testClassInstance, new EventArgs());
            Assert.IsTrue(eventExecuted);
        }

        [TestMethod]
        public void EventCall_CustomDelegate_ByObjectAndType_EventArgsAsObject()
        {
            var call = _testClassType.EventInvoke<object>("CusDelegEvent");
            Assert.IsNotNull(call);
            var eventExecuted = false;
            EventCustomDelegate eventHandler = (sender, args) =>
            {
                eventExecuted = true;
                Assert.IsInstanceOfType(args, typeof(EventArgs));
            };
            _testClassInstance.CusDelegEvent += eventHandler;
            call(_testClassInstance, new EventArgs());
            Assert.IsTrue(eventExecuted);
        }

        [TestMethod]
        public void EventCall_CustomDelegate_ByObjects()
        {
            var call = _testClassType.EventInvoke("CusDelegEvent");
            Assert.IsNotNull(call);
            var eventExecuted = false;
            EventCustomDelegate eventHandler = (sender, args) =>
            {
                eventExecuted = true;
                Assert.IsInstanceOfType(args, typeof(EventArgs));
            };
            _testClassInstance.CusDelegEvent += eventHandler;
            call(_testClassInstance, new EventArgs());
            Assert.IsTrue(eventExecuted);
        }
    } 
#endif
}
