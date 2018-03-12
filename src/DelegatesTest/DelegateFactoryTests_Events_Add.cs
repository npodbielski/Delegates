// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DelegateFactoryTests_Events_Add.cs" company="Natan Podbielski">
//   Copyright (c) 2016 - 2018 Natan Podbielski. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using Delegates;
using DelegatesTest;
using DelegatesTest.TestObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace
#if NET35
        DelegatesTestNET35
#elif NET4
        DelegatesTestNET4
#elif NET45
        DelegatesTestNET45
#elif NET46
        DelegatesTestNET46
#elif PORTABLE
        DelegatesTestNETPortable
#elif NETCOREAPP1_0
    DelegatesTestNETCORE10
#elif NETCOREAPP2_0
        DelegatesTestNETCORE20
#elif NETSTANDARD1_1
        DelegatesTestNETStandard11
#elif NETSTANDARD1_5
        DelegatesTestNETStandard15
#endif
{
    [TestClass]
    public class DelegateFactoryTests_Events_Add
    {
        [TestMethod]
        public void EventAdd_ByTypes_Public()
        {
            var accessor = DelegateFactory.EventAdd<TestClass, TestClass.PublicEventArgs>("PublicEvent");
            Assert.IsNotNull(accessor);
            var eventExecuted = false;
            var testClassInstance = new TestClass();
            accessor(testClassInstance, (sender, args) =>
            {
                eventExecuted = true;
                Assert.IsInstanceOfType(args, typeof(TestClass.PublicEventArgs));
            });
            testClassInstance.InvokePublicEvent();
            Assert.IsTrue(eventExecuted);
        }

        [TestMethod]
        public void EventAdd_ByTypes_Internal()
        {
            var accessor = DelegateFactory.EventAdd<TestClass, TestClass.InternalEventArgs>("InternalEvent");
            Assert.IsNotNull(accessor);
            var eventExecuted = false;
            var testClassInstance = new TestClass();
            accessor(testClassInstance, (sender, args) =>
            {
                eventExecuted = true;
                Assert.IsInstanceOfType(args, typeof(TestClass.InternalEventArgs));
            });
            testClassInstance.InvokeInternalEvent();
            Assert.IsTrue(eventExecuted);
        }

        [TestMethod]
        public void EventAdd_ByTypes_Protected()
        {
            var accessor = DelegateFactory.EventAdd<TestClass, TestClass.ProtectedEventArgs>("ProtectedEvent");
            Assert.IsNotNull(accessor);
            var eventExecuted = false;
            var testClassInstance = new TestClass();
            accessor(testClassInstance, (sender, args) =>
            {
                eventExecuted = true;
                Assert.IsInstanceOfType(args, typeof(TestClass.ProtectedEventArgs));
            });
            testClassInstance.InvokeProtectedEvent();
            Assert.IsTrue(eventExecuted);
        }

        [TestMethod]
        public void EventAdd_ByTypes_Private()
        {
            var accessor = DelegateFactory.EventAdd<TestClass, TestClass.PrivateEventArgs>("PrivateEvent");
            Assert.IsNotNull(accessor);
            var eventExecuted = false;
            var testClassInstance = new TestClass();
            accessor(testClassInstance, (sender, args) =>
            {
                eventExecuted = true;
                Assert.IsInstanceOfType(args, typeof(TestClass.PrivateEventArgs));
            });
            testClassInstance.InvokePrivateEvent();
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
            var accessor = typeof(TestClass).EventAdd<TestClass.PublicEventArgs>("PublicEvent");
            Assert.IsNotNull(accessor);
            var eventExecuted = false;
            var testClassInstance = new TestClass();
            accessor(testClassInstance, (sender, args) =>
            {
                eventExecuted = true;
                Assert.IsInstanceOfType(args, typeof(TestClass.PublicEventArgs));
            });
            testClassInstance.InvokePublicEvent();
            Assert.IsTrue(eventExecuted);
        }

        [TestMethod]
        public void EventAdd_ByObjectAndType_Internal()
        {
            var accessor = typeof(TestClass).EventAdd<TestClass.InternalEventArgs>("InternalEvent");
            Assert.IsNotNull(accessor);
            var eventExecuted = false;
            var testClassInstance = new TestClass();
            accessor(testClassInstance, (sender, args) =>
            {
                eventExecuted = true;
                Assert.IsInstanceOfType(args, typeof(TestClass.InternalEventArgs));
            });
            testClassInstance.InvokeInternalEvent();
            Assert.IsTrue(eventExecuted);
        }

        [TestMethod]
        public void EventAdd_ByObjectAndType_Protected()
        {
            var accessor = typeof(TestClass).EventAdd<TestClass.ProtectedEventArgs>("ProtectedEvent");
            Assert.IsNotNull(accessor);
            var eventExecuted = false;
            var testClassInstance = new TestClass();
            accessor(testClassInstance, (sender, args) =>
            {
                eventExecuted = true;
                Assert.IsInstanceOfType(args, typeof(TestClass.ProtectedEventArgs));
            });
            testClassInstance.InvokeProtectedEvent();
            Assert.IsTrue(eventExecuted);
        }

        [TestMethod]
        public void EventAdd_ByObjectAndType_Private()
        {
            var accessor = typeof(TestClass).EventAdd<TestClass.PrivateEventArgs>("PrivateEvent");
            Assert.IsNotNull(accessor);
            var eventExecuted = false;
            var testClassInstance = new TestClass();
            accessor(testClassInstance, (sender, args) =>
            {
                eventExecuted = true;
                Assert.IsInstanceOfType(args, typeof(TestClass.PrivateEventArgs));
            });
            testClassInstance.InvokePrivateEvent();
            Assert.IsTrue(eventExecuted);
        }

        [TestMethod]
        public void EventAdd_ByObjectAndType_NotExistingEvent()
        {
            var accessor = typeof(TestStruct).EventAdd<TestClass.PrivateEventArgs>("PrivateEvent");
            Assert.IsNull(accessor);
        }

        [TestMethod]
        public void EventAdd_ByObjectAndType_Incorrect_EventType()
        {
            AssertHelper.ThrowsException<ArgumentException>(() =>
            {
                typeof(TestClass).EventAdd<TestClass.PublicEventArgs>("PrivateEvent");
            });
        }

        [TestMethod]
        public void EventAdd_ByObjects_Public()
        {
            var accessor = typeof(TestClass).EventAdd("PublicEvent");
            Assert.IsNotNull(accessor);
            var eventExecuted = false;
            var testClassInstance = new TestClass();
            accessor(testClassInstance, (sender, args) =>
            {
                eventExecuted = true;
                Assert.IsInstanceOfType(args, typeof(TestClass.PublicEventArgs));
            });
            testClassInstance.InvokePublicEvent();
            Assert.IsTrue(eventExecuted);
        }

        [TestMethod]
        public void EventAdd_ByObjects_Internal()
        {
            var accessor = typeof(TestClass).EventAdd("InternalEvent");
            Assert.IsNotNull(accessor);
            var eventExecuted = false;
            var testClassInstance = new TestClass();
            accessor(testClassInstance, (sender, args) =>
            {
                eventExecuted = true;
                Assert.IsInstanceOfType(args, typeof(TestClass.InternalEventArgs));
            });
            testClassInstance.InvokeInternalEvent();
            Assert.IsTrue(eventExecuted);
        }

        [TestMethod]
        public void EventAdd_ByObjects_Protected()
        {
            var accessor = typeof(TestClass).EventAdd("ProtectedEvent");
            Assert.IsNotNull(accessor);
            var eventExecuted = false;
            var testClassInstance = new TestClass();
            accessor(testClassInstance, (sender, args) =>
            {
                eventExecuted = true;
                Assert.IsInstanceOfType(args, typeof(TestClass.ProtectedEventArgs));
            });
            testClassInstance.InvokeProtectedEvent();
            Assert.IsTrue(eventExecuted);
        }

        [TestMethod]
        public void EventAdd_ByObjects_Private()
        {
            var accessor = typeof(TestClass).EventAdd("PrivateEvent");
            Assert.IsNotNull(accessor);
            var eventExecuted = false;
            var testClassInstance = new TestClass();
            accessor(testClassInstance, (sender, args) =>
            {
                eventExecuted = true;
                Assert.IsInstanceOfType(args, typeof(TestClass.PrivateEventArgs));
            });
            testClassInstance.InvokePrivateEvent();
            Assert.IsTrue(eventExecuted);
        }

        [TestMethod]
        public void EventAdd_ByObjects_NotExistingEvent()
        {
            var accessor = typeof(TestStruct).EventAdd("PrivateEvent");
            Assert.IsNull(accessor);
        }

        [TestMethod]
        public void EventAdd_Interface_ByObjects()
        {
            var accessor = typeof(IService).EventAdd("Event");
            Assert.IsNotNull(accessor);
            var eventExecuted = false;

            void EventAction(object sender, object args)
            {
                eventExecuted = true;
                Assert.IsInstanceOfType(args, typeof(EventArgs));
            }

            var interfaceImpl = new Service();
            accessor(interfaceImpl, EventAction);
            interfaceImpl.InvokeEvent();
            Assert.IsTrue(eventExecuted);
        }

        [TestMethod]
        public void EventAdd_Interface_ByObjectAndType()
        {
            var accessor = typeof(IService).EventAdd<EventArgs>("Event");
            Assert.IsNotNull(accessor);
            var eventExecuted = false;

            void EventAction(object sender, EventArgs args)
            {
                eventExecuted = true;
                Assert.IsInstanceOfType(args, typeof(EventArgs));
            }

            var interfaceImpl = new Service();
            accessor(interfaceImpl, EventAction);
            interfaceImpl.InvokeEvent();
            Assert.IsTrue(eventExecuted);
        }

        [TestMethod]
        public void EventAdd_Interface_ByTypes()
        {
            var accessor = DelegateFactory.EventAdd<IService, EventArgs>("Event");
            Assert.IsNotNull(accessor);
            var eventExecuted = false;

            void EventAction(object sender, EventArgs args)
            {
                eventExecuted = true;
                Assert.IsInstanceOfType(args, typeof(EventArgs));
            }

            var interfaceImpl = new Service();
            accessor(interfaceImpl, EventAction);
            interfaceImpl.InvokeEvent();
            Assert.IsTrue(eventExecuted);
        }

        [TestMethod]
        public void EventAdd_CustomDelegate_ByObjects()
        {
            var accessor = typeof(TestClass).EventAdd("CusDelegEvent");
            Assert.IsNotNull(accessor);
            var eventExecuted = false;

            void EventAction(object sender, object args)
            {
                eventExecuted = true;
                Assert.IsInstanceOfType(args, typeof(EventArgs));
            }

            var testClassInstance = new TestClass();
            accessor(testClassInstance, EventAction);
            testClassInstance.InvokeCusDelegEvent();
            Assert.IsTrue(eventExecuted);
        }

        [TestMethod]
        public void EventAdd_CustomDelegate_ByObjectAndType()
        {
            var accessor = typeof(TestClass).EventAdd<EventArgs>("CusDelegEvent");
            Assert.IsNotNull(accessor);
            var eventExecuted = false;

            void EventAction(object sender, EventArgs args)
            {
                eventExecuted = true;
                Assert.IsInstanceOfType(args, typeof(EventArgs));
            }

            var testClassInstance = new TestClass();
            accessor(testClassInstance, EventAction);
            testClassInstance.InvokeCusDelegEvent();
            Assert.IsTrue(eventExecuted);
        }

        [TestMethod]
        public void EventAdd_CustomDelegate_ByTypeAndObject()
        {
            var accessor = DelegateFactory.EventAdd<TestClass>("CusDelegEvent");
            Assert.IsNotNull(accessor);
            var eventExecuted = false;

            void EventAction(TestClass sender, object args)
            {
                eventExecuted = true;
                Assert.IsInstanceOfType(args, typeof(EventArgs));
            }

            var testClassInstance = new TestClass();
            accessor(testClassInstance, EventAction);
            testClassInstance.InvokeCusDelegEvent();
            Assert.IsTrue(eventExecuted);
        }

        [TestMethod]
        public void EventAdd_CustomDelegate_ByTypesAndEventHandler()
        {
            var accessor = DelegateFactory.EventAdd<TestClass, EventArgs>("CusDelegEvent");
            Assert.IsNotNull(accessor);
            var eventExecuted = false;

            void EventAction(object sender, EventArgs args)
            {
                eventExecuted = true;
                Assert.IsInstanceOfType(args, typeof(EventArgs));
            }

            var testClassInstance = new TestClass();
            accessor(testClassInstance, EventAction);
            testClassInstance.InvokeCusDelegEvent();
            Assert.IsTrue(eventExecuted);
        }

        [TestMethod]
        public void EventAdd_CustomDelegate_ByTypes()
        {
            var accessor = DelegateFactory.EventAddCustomDelegate<TestClass, EventCustomDelegate>("CusDelegEvent");
            Assert.IsNotNull(accessor);
            var eventExecuted = false;

            void EventAction(object sender, EventArgs args)
            {
                eventExecuted = true;
                Assert.IsInstanceOfType(args, typeof(EventArgs));
            }

            var testClassInstance = new TestClass();
            accessor(testClassInstance, EventAction);
            testClassInstance.InvokeCusDelegEvent();
            Assert.IsTrue(eventExecuted);
        }

        [TestMethod]
        public void EventAdd_CustomDelegate_IncompatibleDelegate()
        {
            ArgumentException ae = null;
            try
            {
                DelegateFactory
                    .EventAddCustomDelegate<TestClass, EventHandler<ConsoleCancelEventArgs>>("CusDelegEvent");
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