// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DelegateFactoryTests_Events_Invoke.cs" company="Natan Podbielski">
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
    public class DelegateFactoryTests_Events_Invoke
    {
        [TestMethod]
        public void EventInvoke_ByTypes_Public()
        {
            var call = DelegateFactory.EventInvoke<TestClass, TestClass.PublicEventArgs>("PublicEvent");
            Assert.IsNotNull(call);
            var eventExecuted = false;

            void EventHandler(object sender, TestClass.PublicEventArgs args)
            {
                eventExecuted = true;
                Assert.IsInstanceOfType(args, typeof(TestClass.PublicEventArgs));
            }

            var testClassInstance = new TestClass();
            testClassInstance.AddPublicEventHandler(EventHandler);
            call(testClassInstance, new TestClass.PublicEventArgs());
            Assert.IsTrue(eventExecuted);
        }

        [TestMethod]
        public void EventInvoke_ByTypes_Internal()
        {
            var call = DelegateFactory.EventInvoke<TestClass, TestClass.InternalEventArgs>("InternalEventBackend");
            Assert.IsNotNull(call);
            var eventExecuted = false;

            void EventHandler(object sender, TestClass.InternalEventArgs args)
            {
                eventExecuted = true;
                Assert.IsInstanceOfType(args, typeof(TestClass.InternalEventArgs));
            }

            var testClassInstance = new TestClass();
            testClassInstance.AddInternalEventHandler(EventHandler);
            call(testClassInstance, new TestClass.InternalEventArgs());
            Assert.IsTrue(eventExecuted);
        }

        [TestMethod]
        public void EventInvoke_ByTypes_Protected()
        {
            var call = DelegateFactory.EventInvoke<TestClass, TestClass.ProtectedEventArgs>("ProtectedEvent");
            Assert.IsNotNull(call);
            var eventExecuted = false;

            void EventHandler(object sender, TestClass.ProtectedEventArgs args)
            {
                eventExecuted = true;
                Assert.IsInstanceOfType(args, typeof(TestClass.ProtectedEventArgs));
            }

            var testClassInstance = new TestClass();
            testClassInstance.AddProtectedEventHandler(EventHandler);
            call(testClassInstance, new TestClass.ProtectedEventArgs());
            Assert.IsTrue(eventExecuted);
        }

        [TestMethod]
        public void EventInvoke_ByTypes_Private()
        {
            var call = DelegateFactory.EventInvoke<TestClass, TestClass.PrivateEventArgs>("PrivateEvent");
            Assert.IsNotNull(call);
            var eventExecuted = false;

            void EventHandler(object sender, TestClass.PrivateEventArgs args)
            {
                eventExecuted = true;
                Assert.IsInstanceOfType(args, typeof(TestClass.PrivateEventArgs));
            }

            var testClassInstance = new TestClass();
            testClassInstance.AddPrivateEventHandler(EventHandler);
            call(testClassInstance, new TestClass.PrivateEventArgs());
            Assert.IsTrue(eventExecuted);
        }

        [TestMethod]
        public void EventInvoke_ByTypes_EventArgsAsObject()
        {
            var call = DelegateFactory.EventInvoke<TestClass, object>("PublicEvent");
            Assert.IsNotNull(call);
            var eventExecuted = false;

            void EventHandler(object sender, TestClass.PublicEventArgs args)
            {
                eventExecuted = true;
                Assert.IsInstanceOfType(args, typeof(TestClass.PublicEventArgs));
            }

            var testClassInstance = new TestClass();
            testClassInstance.AddPublicEventHandler(EventHandler);
            call(testClassInstance, new TestClass.PublicEventArgs());
            Assert.IsTrue(eventExecuted);
        }

        [TestMethod]
        public void EventInvoke_ByTypes_NoEvent()
        {
            var call = DelegateFactory.EventInvoke<TestStruct, TestClass.PublicEventArgs>("PublicEvent");
            Assert.IsNull(call);
        }

        [TestMethod]
        public void EventInvoke_ByTypes_WrongName()
        {
            var call = DelegateFactory.EventInvoke<TestClass, TestClass.PublicEventArgs>("WrongName");
            Assert.IsNull(call);
        }

        [TestMethod]
        public void EventInvoke_ByTypes_IncorrectEventType()
        {
            AssertHelper.ThrowsException<ArgumentException>(() =>
            {
                DelegateFactory.EventInvoke<TestClass, TestClass.PrivateEventArgs>("PublicEvent");
            });
        }

        /// <summary>
        ///     Tests null checking in EventInvoke delegate (cant call event if there is not handlers attached)
        /// </summary>
        [TestMethod]
        public void EventInvoke_ByTypes_Public_EventIsNull()
        {
            var call = DelegateFactory.EventInvoke<TestClass, TestClass.PublicEventArgs>("PublicEvent");
            Assert.IsNotNull(call);
            call(new TestClass(), new TestClass.PublicEventArgs());
        }

        [TestMethod]
        public void EventInvoke_ByTypes_Internal_EventIsNull()
        {
            var call = DelegateFactory.EventInvoke<TestClass, TestClass.InternalEventArgs>("InternalEventBackend");
            Assert.IsNotNull(call);
            call(new TestClass(), new TestClass.InternalEventArgs());
        }

        [TestMethod]
        public void EventInvoke_ByTypes_Protected_EventIsNull()
        {
            var call = DelegateFactory.EventInvoke<TestClass, TestClass.ProtectedEventArgs>("ProtectedEvent");
            Assert.IsNotNull(call);
            call(new TestClass(), new TestClass.ProtectedEventArgs());
        }

        [TestMethod]
        public void EventInvoke_ByTypes_Private_EventIsNull()
        {
            var call = DelegateFactory.EventInvoke<TestClass, TestClass.PrivateEventArgs>("PrivateEvent");
            Assert.IsNotNull(call);
            call(new TestClass(), new TestClass.PrivateEventArgs());
        }

        [TestMethod]
        public void EventInvoke_ByObjectAndType_Public()
        {
            var call = typeof(TestClass).EventInvoke<TestClass.PublicEventArgs>("PublicEvent");
            Assert.IsNotNull(call);
            var eventExecuted = false;

            void EventHandler(object sender, TestClass.PublicEventArgs args)
            {
                eventExecuted = true;
                Assert.IsInstanceOfType(args, typeof(TestClass.PublicEventArgs));
            }

            var testClassInstance = new TestClass();
            testClassInstance.AddPublicEventHandler(EventHandler);
            call(testClassInstance, new TestClass.PublicEventArgs());
            Assert.IsTrue(eventExecuted);
        }

        [TestMethod]
        public void EventInvoke_ByObjectAndType_Internal()
        {
            var call = typeof(TestClass).EventInvoke<TestClass.InternalEventArgs>("InternalEventBackend");
            Assert.IsNotNull(call);
            var eventExecuted = false;

            void EventHandler(object sender, TestClass.InternalEventArgs args)
            {
                eventExecuted = true;
                Assert.IsInstanceOfType(args, typeof(TestClass.InternalEventArgs));
            }

            var testClassInstance = new TestClass();
            testClassInstance.AddInternalEventHandler(EventHandler);
            call(testClassInstance, new TestClass.InternalEventArgs());
            Assert.IsTrue(eventExecuted);
        }

        [TestMethod]
        public void EventInvoke_ByObjectAndType_Protected()
        {
            var call = typeof(TestClass).EventInvoke<TestClass.ProtectedEventArgs>("ProtectedEvent");
            Assert.IsNotNull(call);
            var eventExecuted = false;

            void EventHandler(object sender, TestClass.ProtectedEventArgs args)
            {
                eventExecuted = true;
                Assert.IsInstanceOfType(args, typeof(TestClass.ProtectedEventArgs));
            }

            var testClassInstance = new TestClass();
            testClassInstance.AddProtectedEventHandler(EventHandler);
            call(testClassInstance, new TestClass.ProtectedEventArgs());
            Assert.IsTrue(eventExecuted);
        }

        [TestMethod]
        public void EventInvoke_ByObjectAndType_Private()
        {
            var call = typeof(TestClass).EventInvoke<TestClass.PrivateEventArgs>("PrivateEvent");
            Assert.IsNotNull(call);
            var eventExecuted = false;

            void EventHandler(object sender, TestClass.PrivateEventArgs args)
            {
                eventExecuted = true;
                Assert.IsInstanceOfType(args, typeof(TestClass.PrivateEventArgs));
            }

            var testClassInstance = new TestClass();
            testClassInstance.AddPrivateEventHandler(EventHandler);
            call(testClassInstance, new TestClass.PrivateEventArgs());
            Assert.IsTrue(eventExecuted);
        }

        [TestMethod]
        public void EventInvoke_ByObjectAndType_NoEvent()
        {
            var call = typeof(TestStruct).EventInvoke<TestClass.PublicEventArgs>("PublicEvent");
            Assert.IsNull(call);
        }

        [TestMethod]
        public void EventInvoke_ByObjectAndType_WrongName()
        {
            var call = typeof(TestClass).EventInvoke<TestClass.PublicEventArgs>("WrongName");
            Assert.IsNull(call);
        }

        [TestMethod]
        public void EventInvoke_ByObjectAndType_IncorrectEventType()
        {
            AssertHelper.ThrowsException<ArgumentException>(() =>
            {
                typeof(TestClass).EventInvoke<TestClass.PrivateEventArgs>("PublicEvent");
            });
        }

        [TestMethod]
        public void EventInvoke_ByObjectAndType_EventArgsAsObject()
        {
            var call = typeof(TestClass).EventInvoke<object>("PublicEvent");
            Assert.IsNotNull(call);
            var eventExecuted = false;

            void EventHandler(object sender, TestClass.PublicEventArgs args)
            {
                eventExecuted = true;
                Assert.IsInstanceOfType(args, typeof(TestClass.PublicEventArgs));
            }

            var testClassInstance = new TestClass();
            testClassInstance.AddPublicEventHandler(EventHandler);
            call(testClassInstance, new TestClass.PublicEventArgs());
            Assert.IsTrue(eventExecuted);
        }

        /// <summary>
        ///     Tests null checking in EventInvoke delegate (cant call event if there is not handlers attached)
        /// </summary>
        [TestMethod]
        public void EventInvoke_ByObjectAndType_Public_EventIsNull()
        {
            var call = DelegateFactory.EventInvoke<TestClass, TestClass.PublicEventArgs>("PublicEvent");
            Assert.IsNotNull(call);
            call(new TestClass(), new TestClass.PublicEventArgs());
        }

        [TestMethod]
        public void EventInvoke_ByObjectAndType_Internal_EventIsNull()
        {
            var call = DelegateFactory.EventInvoke<TestClass, TestClass.InternalEventArgs>("InternalEventBackend");
            Assert.IsNotNull(call);
            call(new TestClass(), new TestClass.InternalEventArgs());
        }

        [TestMethod]
        public void EventInvoke_ByObjectAndType_Protected_EventIsNull()
        {
            var call = DelegateFactory.EventInvoke<TestClass, TestClass.ProtectedEventArgs>("ProtectedEvent");
            Assert.IsNotNull(call);
            call(new TestClass(), new TestClass.ProtectedEventArgs());
        }

        [TestMethod]
        public void EventInvoke_ByObjectAndType_Private_EventIsNull()
        {
            var call = DelegateFactory.EventInvoke<TestClass, TestClass.PrivateEventArgs>("PrivateEvent");
            Assert.IsNotNull(call);
            call(new TestClass(), new TestClass.PrivateEventArgs());
        }

        [TestMethod]
        public void EventInvoke_ByObjects_Public()
        {
            var call = typeof(TestClass).EventInvoke("PublicEvent");
            Assert.IsNotNull(call);
            var eventExecuted = false;

            void EventHandler(object sender, TestClass.PublicEventArgs args)
            {
                eventExecuted = true;
                Assert.IsInstanceOfType(args, typeof(TestClass.PublicEventArgs));
            }

            var testClassInstance = new TestClass();
            testClassInstance.AddPublicEventHandler(EventHandler);
            call(testClassInstance, new TestClass.PublicEventArgs());
            Assert.IsTrue(eventExecuted);
        }

        [TestMethod]
        public void EventInvoke_ByObjects_Internal()
        {
            var call = typeof(TestClass).EventInvoke("InternalEventBackend");
            Assert.IsNotNull(call);
            var eventExecuted = false;

            void EventHandler(object sender, TestClass.InternalEventArgs args)
            {
                eventExecuted = true;
                Assert.IsInstanceOfType(args, typeof(TestClass.InternalEventArgs));
            }

            var testClassInstance = new TestClass();
            testClassInstance.AddInternalEventHandler(EventHandler);
            call(testClassInstance, new TestClass.InternalEventArgs());
            Assert.IsTrue(eventExecuted);
        }

        [TestMethod]
        public void EventInvoke_ByObjects_Protected()
        {
            var call = typeof(TestClass).EventInvoke("ProtectedEvent");
            Assert.IsNotNull(call);
            var eventExecuted = false;

            void EventHandler(object sender, TestClass.ProtectedEventArgs args)
            {
                eventExecuted = true;
                Assert.IsInstanceOfType(args, typeof(TestClass.ProtectedEventArgs));
            }

            var testClassInstance = new TestClass();
            testClassInstance.AddProtectedEventHandler(EventHandler);
            call(testClassInstance, new TestClass.ProtectedEventArgs());
            Assert.IsTrue(eventExecuted);
        }

        [TestMethod]
        public void EventInvoke_ByObjects_Private()
        {
            var call = typeof(TestClass).EventInvoke("PrivateEvent");
            Assert.IsNotNull(call);
            var eventExecuted = false;

            void EventHandler(object sender, TestClass.PrivateEventArgs args)
            {
                eventExecuted = true;
                Assert.IsInstanceOfType(args, typeof(TestClass.PrivateEventArgs));
            }

            var testClassInstance = new TestClass();
            testClassInstance.AddPrivateEventHandler(EventHandler);
            call(testClassInstance, new TestClass.PrivateEventArgs());
            Assert.IsTrue(eventExecuted);
        }

        [TestMethod]
        public void EventInvoke_ByObjects_NoEvent()
        {
            var call = typeof(TestStruct).EventInvoke("PublicEvent");
            Assert.IsNull(call);
        }

        [TestMethod]
        public void EventInvoke_ByObjects_WrongName()
        {
            var call = typeof(TestClass).EventInvoke("WrongName");
            Assert.IsNull(call);
        }

        [TestMethod]
        public void EventInvoke_ByObjects_IncorrectEventType()
        {
            AssertHelper.ThrowsException<ArgumentException>(() =>
            {
                typeof(TestClass).EventInvoke<TestClass.PrivateEventArgs>("PublicEvent");
            });
        }

        /// <summary>
        ///     Tests null checking in EventInvoke delegate (cant call event if there is not handlers attached)
        /// </summary>
        [TestMethod]
        public void EventInvoke_ByObjects_Public_EventIsNull()
        {
            var call = DelegateFactory.EventInvoke<TestClass, TestClass.PublicEventArgs>("PublicEvent");
            Assert.IsNotNull(call);
            call(new TestClass(), new TestClass.PublicEventArgs());
        }

        [TestMethod]
        public void EventInvoke_ByObjects_Internal_EventIsNull()
        {
            var call = DelegateFactory.EventInvoke<TestClass, TestClass.InternalEventArgs>("InternalEventBackend");
            Assert.IsNotNull(call);
            call(new TestClass(), new TestClass.InternalEventArgs());
        }

        [TestMethod]
        public void EventInvoke_ByObjects_Protected_EventIsNull()
        {
            var call = DelegateFactory.EventInvoke<TestClass, TestClass.ProtectedEventArgs>("ProtectedEvent");
            Assert.IsNotNull(call);
            call(new TestClass(), new TestClass.ProtectedEventArgs());
        }

        [TestMethod]
        public void EventInvoke_ByObjects_Private_EventIsNull()
        {
            var call = DelegateFactory.EventInvoke<TestClass, TestClass.PrivateEventArgs>("PrivateEvent");
            Assert.IsNotNull(call);
            call(new TestClass(), new TestClass.PrivateEventArgs());
        }

        [TestMethod]
        public void EventInvoke_ByTypes_CustomEventDelegate()
        {
            var call = DelegateFactory.EventInvoke<TestClass, EventArgs>("CusDelegEvent");
            Assert.IsNotNull(call);
            var eventExecuted = false;

            void EventHandler(object sender, EventArgs args)
            {
                eventExecuted = true;
                Assert.IsInstanceOfType(args, typeof(EventArgs));
            }

            var testClassInstance = new TestClass();
            testClassInstance.CusDelegEvent += EventHandler;
            call(testClassInstance, new EventArgs());
            Assert.IsTrue(eventExecuted);
        }

        [TestMethod]
        public void EventInvoke_ByTypeAndCustomDelegate()
        {
            var call = DelegateFactory.EventInvokeCustomDelegate<TestClass, EventCustomDelegate>("CusDelegEvent");
            Assert.IsNotNull(call);
            var eventExecuted = false;

            void EventHandler(object sender, EventArgs args)
            {
                eventExecuted = true;
                Assert.IsInstanceOfType(args, typeof(EventArgs));
            }

            var testClassInstance = new TestClass();
            testClassInstance.CusDelegEvent += EventHandler;
            call(testClassInstance, new EventArgs());
            Assert.IsTrue(eventExecuted);
        }

        [TestMethod]
        public void EventInvoke_ByTypeAndCustomDelegate_IncorrectEventType()
        {
            AssertHelper.ThrowsException<ArgumentException>(() =>
            {
                DelegateFactory.EventInvokeCustomDelegate<TestClass, EventHandler<TestClass.PrivateEventArgs>>("PublicEvent");
            });
        }

        [TestMethod]
        public void EventInvoke_ByTypeAndCustomDelegate_CompatibleDelegate()
        {
            var call = DelegateFactory.EventInvokeCustomDelegate<TestClass, EventHandler<EventArgs>>
                ("CusDelegEvent");
            Assert.IsNotNull(call);
            var eventExecuted = false;

            void EventHandler(object sender, EventArgs args)
            {
                eventExecuted = true;
                Assert.IsInstanceOfType(args, typeof(EventArgs));
            }

            var testClassInstance = new TestClass();
            testClassInstance.CusDelegEvent += EventHandler;
            call(testClassInstance, new EventArgs());
            Assert.IsTrue(eventExecuted);
        }

        [TestMethod]
        public void EventInvoke_ByObjectAndCustomDelegate()
        {
            var call = typeof(TestClass).EventInvokeCustomDelegate<EventCustomDelegate>("CusDelegEvent");
            Assert.IsNotNull(call);
            var eventExecuted = false;

            void EventHandler(object sender, EventArgs args)
            {
                eventExecuted = true;
                Assert.IsInstanceOfType(args, typeof(EventArgs));
            }

            var testClassInstance = new TestClass();
            testClassInstance.CusDelegEvent += EventHandler;
            call(testClassInstance, new EventArgs());
            Assert.IsTrue(eventExecuted);
        }

        [TestMethod]
        public void EventInvoke_ByObjectAndCustomDelegate_CompatibleDelegate()
        {
            var call = typeof(TestClass).EventInvokeCustomDelegate<EventHandler<EventArgs>>("CusDelegEvent");
            Assert.IsNotNull(call);
            var eventExecuted = false;

            void EventHandler(object sender, EventArgs args)
            {
                eventExecuted = true;
                Assert.IsInstanceOfType(args, typeof(EventArgs));
            }

            var testClassInstance = new TestClass();
            testClassInstance.CusDelegEvent += EventHandler;
            call(testClassInstance, new EventArgs());
            Assert.IsTrue(eventExecuted);
        }

        [TestMethod]
        public void EventInvoke_CustomDelegate_ByTypes()
        {
            var call = DelegateFactory.EventInvoke<TestClass, EventArgs>("CusDelegEvent");
            Assert.IsNotNull(call);
            var eventExecuted = false;

            void EventHandler(object sender, EventArgs args)
            {
                eventExecuted = true;
                Assert.IsInstanceOfType(args, typeof(EventArgs));
            }

            var testClassInstance = new TestClass();
            testClassInstance.CusDelegEvent += EventHandler;
            call(testClassInstance, new EventArgs());
            Assert.IsTrue(eventExecuted);
        }

        [TestMethod]
        public void EventInvoke_CustomDelegate_ByTypes_EventArgsAsObject()
        {
            var call = DelegateFactory.EventInvoke<TestClass, object>("CusDelegEvent");
            Assert.IsNotNull(call);
            var eventExecuted = false;

            void EventHandler(object sender, EventArgs args)
            {
                eventExecuted = true;
                Assert.IsInstanceOfType(args, typeof(EventArgs));
            }

            var testClassInstance = new TestClass();
            testClassInstance.CusDelegEvent += EventHandler;
            call(testClassInstance, new EventArgs());
            Assert.IsTrue(eventExecuted);
        }

        [TestMethod]
        public void EventInvoke_CustomDelegate_ByObjectAndType_EventArgsAsObject()
        {
            var call = typeof(TestClass).EventInvoke<object>("CusDelegEvent");
            Assert.IsNotNull(call);
            var eventExecuted = false;

            void EventHandler(object sender, EventArgs args)
            {
                eventExecuted = true;
                Assert.IsInstanceOfType(args, typeof(EventArgs));
            }

            var testClassInstance = new TestClass();
            testClassInstance.CusDelegEvent += EventHandler;
            call(testClassInstance, new EventArgs());
            Assert.IsTrue(eventExecuted);
        }

        [TestMethod]
        public void EventInvoke_CustomDelegate_ByObjects()
        {
            var call = typeof(TestClass).EventInvoke("CusDelegEvent");
            Assert.IsNotNull(call);
            var eventExecuted = false;

            void EventHandler(object sender, EventArgs args)
            {
                eventExecuted = true;
                Assert.IsInstanceOfType(args, typeof(EventArgs));
            }

            var testClassInstance = new TestClass();
            testClassInstance.CusDelegEvent += EventHandler;
            call(testClassInstance, new EventArgs());
            Assert.IsTrue(eventExecuted);
        }
    }
}