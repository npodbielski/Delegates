using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.InteropServices;
using Delegates.Extensions;

namespace Delegates
{
    class ConsoleApplication
    {
        private static Stopwatch _stopWatch;
        private static double _delay = 1e8;
        private static readonly TestClass TestInstance = new TestClass();
        private static readonly Type Type = typeof(TestClass);

        static void Main(string[] args)
        {
            TestIndexers();
            TestEvents();
            TestProperties();
            TestStaticProperties();
            TestFields();
            TestStaticFields();
            TestInstanceMethods();
            TestStaticMethods();
            TestConstructors();
        }

        private static void TestConstructors()
        {
            var cd = DelegateFactory.DefaultContructor<TestClass>();
            var c1 = DelegateFactory.Contructor<TestClass, Func<TestClass>>();
            var c2 = DelegateFactory.Contructor<TestClass, Func<int, TestClass>>();
            var c3 = DelegateFactory.Contructor<TestClass, Func<bool, TestClass>>();
            var c4 = DelegateFactory.Contructor<TestClass, Func<string, TestClass>>();

            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            for (var i = 0; i < _delay; i++)
            {
                var test = new TestClass();
            }
            _stopWatch.Stop();
            Console.WriteLine("Public default constructor directly: {0}", _stopWatch.ElapsedMilliseconds);

            var constructorInfo = Type.GetConstructor(Type.EmptyTypes);
            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            for (var i = 0; i < _delay; i++)
            {
                var test = (TestClass)constructorInfo.Invoke(null);
            }
            _stopWatch.Stop();
            Console.WriteLine("Public default constructor via reflection: {0}", _stopWatch.ElapsedMilliseconds);

            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            for (var i = 0; i < _delay; i++)
            {
                var test = Activator.CreateInstance<TestClass>();
            }
            _stopWatch.Stop();
            Console.WriteLine("Public default constructor via Activator: {0}", _stopWatch.ElapsedMilliseconds);


            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            for (var i = 0; i < _delay; i++)
            {
                var t = c1();
            }
            _stopWatch.Stop();
            Console.WriteLine("Public default constructor proxy: {0}", _stopWatch.ElapsedMilliseconds);

            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            for (var i = 0; i < _delay; i++)
            {
                var t = c2(0);
            }
            _stopWatch.Stop();
            Console.WriteLine("Internal constructor proxy: {0}", _stopWatch.ElapsedMilliseconds);

            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            for (var i = 0; i < _delay; i++)
            {
                var t = c3(false);
            }
            _stopWatch.Stop();
            Console.WriteLine("Protected constructor proxy: {0}", _stopWatch.ElapsedMilliseconds);

            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            for (var i = 0; i < _delay; i++)
            {
                var t = c4("");
            }
            _stopWatch.Stop();
            Console.WriteLine("Private constructor proxy: {0}", _stopWatch.ElapsedMilliseconds);
        }

        private static void TestIndexers()
        {
            var ig1 = DelegateFactory.IndexerGet<TestClass, int, int>();
            var ig2 = DelegateFactory.IndexerGet<TestClass, string, string>();
            var ig3 = DelegateFactory.IndexerGet<TestClass, long, long>();
            var ig4 = Type.IndexerGet<int, int>();
            var ig5 = Type.IndexerGet<int, int, int>();
            var ig6 = Type.IndexerGet<int, int, int, int>();
            var ig7 = Type.IndexerGet(typeof(int), typeof(int));
            var ig8 = DelegateFactory.IndexerGet<TestClass, int, int, int>();
            var ig9 = DelegateFactory.IndexerGet<TestClass, int, int, int, int>();
            var ig10 = Type.IndexerGet(typeof(int), typeof(int), typeof(int), typeof(int));

            var t = ig4(TestInstance, 0);
            var t2 = ig5(TestInstance, 0, 0);
            var t3 = ig6(TestInstance, 0, 0, 0);
            var t4 = ig7(TestInstance, 0);
            var t5 = ig8(TestInstance, 0, 0);
            var t6 = ig9(TestInstance, 0, 0, 0);
            var t7 = ig10(TestInstance, new object[] { 0, 0, 0 });

            var is1 = DelegateFactory.IndexerSet<TestClass, int, int>();
            var is2 = DelegateFactory.IndexerSet<TestClass, int, int, int>();
            var is3 = DelegateFactory.IndexerSet<TestClass, int, int, int, int>();
            var is4 = Type.IndexerSet(typeof(int), typeof(int), typeof(int), typeof(int));
            var is5 = Type.IndexerSet<int, int>();
            var is6 = Type.IndexerSet<int, int, int>();
            var is7 = Type.IndexerSet<int, int, int, int>();

            is1(TestInstance, 0, 1);
            Console.WriteLine(TestInstance[0]);
            is2(TestInstance, 0, 0, 1);
            is3(TestInstance, 0, 0, 0, 1);
            is4(TestInstance, new object[] { 0, 0, 0 }, 1);
            is5(TestInstance, 1, 2);
            Console.WriteLine(TestInstance[1]);
            is6(TestInstance, 0, 0, 2);
            is7(TestInstance, 0, 0, 0, 2);


            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            for (var i = 0; i < _delay; i++)
            {
                var test = TestInstance[0];
            }
            _stopWatch.Stop();
            Console.WriteLine("Public indexer: {0}", _stopWatch.ElapsedMilliseconds);

            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            for (var i = 0; i < _delay; i++)
            {
                var test = ig1(TestInstance, 0);
            }
            _stopWatch.Stop();
            Console.WriteLine("Public indexer retriever: {0}", _stopWatch.ElapsedMilliseconds);

            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            for (var i = 0; i < _delay; i++)
            {
                var test = ig2(TestInstance, "s");
            }
            _stopWatch.Stop();
            Console.WriteLine("Internal indexer retriever: {0}", _stopWatch.ElapsedMilliseconds);

            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            for (var i = 0; i < _delay; i++)
            {
                var test = ig3(TestInstance, i);
            }
            _stopWatch.Stop();
            Console.WriteLine("Private indexer: {0}", _stopWatch.ElapsedMilliseconds);

            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            for (var i = 0; i < _delay; i++)
            {
                var test = TestInstance[0, 0, 0];
            }
            _stopWatch.Stop();
            Console.WriteLine("Multiple index indexer directly: {0}", _stopWatch.ElapsedMilliseconds);

            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            for (var i = 0; i < _delay; i++)
            {
                var test = ig10(TestInstance, new object[] { 0, 0, 0 });
            }
            _stopWatch.Stop();
            Console.WriteLine("Multiple index indexer via delegate with array: {0}", _stopWatch.ElapsedMilliseconds);

            var indexerInfo = Type.GetProperty("TheItem", typeof(int), new Type[] { typeof(int), typeof(int), typeof(int) });
            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            for (var i = 0; i < _delay; i++)
            {
                var test = indexerInfo.GetValue(TestInstance, new object[] { 0, 0, 0 });
            }
            _stopWatch.Stop();
            Console.WriteLine("Multiple index indexer via reflection: {0}", _stopWatch.ElapsedMilliseconds);
        }

        private static void TestEvents()
        {
            var ea1 = DelegateFactory.EventAdd<TestClass, TestClass.PublicEventArgs>("PublicEvent");
            var ea2 = DelegateFactory.EventAdd<TestClass, TestClass.InternalEventArgs>("InternalEvent");
            var ea3 = DelegateFactory.EventAdd<TestClass>("ProtectedEvent");
            var ea4 = Type.EventAdd("PrivateEvent");

            var er1 = DelegateFactory.EventRemove<TestClass, TestClass.PublicEventArgs>("PublicEvent");
            var er2 = DelegateFactory.EventRemove<TestClass, TestClass.InternalEventArgs>("InternalEvent");
            var er3 = DelegateFactory.EventRemove<TestClass>("ProtectedEvent");
            var er4 = Type.EventRemove("PrivateEvent");

            ea1(TestInstance, TestHandler);
            ea2(TestInstance, TestHandler);
            ea3(TestInstance, TestHandler);
            ea4(TestInstance, TestHandler);

            TestInstance.InvokePublicEvent();
            TestInstance.InvokeInternalEvent();
            TestInstance.InvokeProtectedEvent();
            TestInstance.InvokePrivateEvent();

            er1(TestInstance, TestHandler);
            er2(TestInstance, TestHandler);
            er3(TestInstance, TestHandler);
            er4(TestInstance, TestHandler);

            TestInstance.InvokePublicEvent();
            TestInstance.InvokeInternalEvent();
            TestInstance.InvokeProtectedEvent();
            TestInstance.InvokePrivateEvent();
        }

        /// 
        /// find a way to create handler automatically since we could not have access to EventArgs type becaue it 
        /// may be private (event if this would not make sense)
        /// 

        private static void TestHandler(TestClass sender, object eventArgs)
        {
            if (eventArgs is TestClass.PublicEventArgs)
            {
                Console.WriteLine("Public handler works!");
            }
            else if (eventArgs is TestClass.InternalEventArgs)
            {
                Console.WriteLine("Internal handler works!");
            }
            else if (eventArgs.GetType() ==
                Type.GetNestedType("ProtectedEventArgs", BindingFlags.Instance | BindingFlags.NonPublic))
            {
                Console.WriteLine("Protected handler works!");
            }
            else if (eventArgs.GetType() ==
                Type.GetNestedType("PrivateEventArgs", BindingFlags.Instance | BindingFlags.NonPublic))
            {
                Console.WriteLine("Private handler works!");
            }
        }

        private static void TestHandler(object sender, object eventArgs)
        {
            if (eventArgs is TestClass.PublicEventArgs)
            {
                Console.WriteLine("Public handler works!");
            }
            else if (eventArgs is TestClass.InternalEventArgs)
            {
                Console.WriteLine("Internal handler works!");
            }
            else if (eventArgs.GetType() ==
                Type.GetNestedType("ProtectedEventArgs", BindingFlags.Instance | BindingFlags.NonPublic))
            {
                Console.WriteLine("Protected handler works!");
            }
            else if (eventArgs.GetType() ==
                Type.GetNestedType("PrivateEventArgs", BindingFlags.Instance | BindingFlags.NonPublic))
            {
                Console.WriteLine("Private handler works!");
            }
        }

        private static void TestInstanceMethods()
        {
            var m1 = DelegateFactory.InstanceMethod<Func<TestClass, string, string>>("PublicMethod");
            var m2 = DelegateFactory.InstanceMethod<Func<TestClass, string, string>>("InternalMethod");
            var m3 = DelegateFactory.InstanceMethod<Func<TestClass, string, string>>("ProtectedMethod");
            var m4 = DelegateFactory.InstanceMethod<Func<TestClass, string, string>>("PrivateMethod");
            var m11 = DelegateFactory.InstanceMethod2<Func<TestClass, string, string>>("PublicMethod");

            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            for (var i = 0; i < _delay; i++)
            {
                var test = TestInstance.PublicMethod("test");
            }
            _stopWatch.Stop();
            Console.WriteLine("Public method directly: {0}", _stopWatch.ElapsedMilliseconds);

            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            for (var i = 0; i < _delay; i++)
            {
                var test = m1(TestInstance, "test");
            }
            _stopWatch.Stop();
            Console.WriteLine("Public method proxy: {0}", _stopWatch.ElapsedMilliseconds);

            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            for (var i = 0; i < 10000; i++)
            {
                DelegateFactory.InstanceMethod<Func<TestClass, string, string>>("PrivateMethod");
            }
            _stopWatch.Stop();
            Console.WriteLine("Static private method proxy creator via reflection: {0}", _stopWatch.ElapsedMilliseconds);

            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            for (var i = 0; i < 10000; i++)
            {
                DelegateFactory.InstanceMethod2<Func<TestClass, string, string>>("PrivateMethod");
            }
            _stopWatch.Stop();
            Console.WriteLine("Static private method proxy creator via expression: {0}", _stopWatch.ElapsedMilliseconds);
        }

        private static void TestStaticMethods()
        {
            var sm1 = Type.StaticMethod<Func<string, string>>("StaticPublicMethod");
            var sm2 = Type.StaticMethod<Func<string, string>>("StaticInternalMethod");
            var sm3 = Type.StaticMethod<Func<string, string>>("StaticProtectedMethod");
            var sm4 = Type.StaticMethod<Func<string, string>>("StaticPrivateMethod");

            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            for (var i = 0; i < _delay; i++)
            {
                var test = sm1("test");
            }
            _stopWatch.Stop();
            Console.WriteLine("Static Public method proxy: {0}", _stopWatch.ElapsedMilliseconds);

            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            for (var i = 0; i < _delay; i++)
            {
                var test = sm2("test");
            }
            _stopWatch.Stop();
            Console.WriteLine("Static internal method proxy: {0}", _stopWatch.ElapsedMilliseconds);

            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            for (var i = 0; i < _delay; i++)
            {
                var test = sm3("test");
            }
            _stopWatch.Stop();
            Console.WriteLine("Static protected method proxy: {0}", _stopWatch.ElapsedMilliseconds);

            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            for (var i = 0; i < _delay; i++)
            {
                var test = sm4("test");
            }
            _stopWatch.Stop();
            Console.WriteLine("Static private method proxy: {0}", _stopWatch.ElapsedMilliseconds);

            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            for (var i = 0; i < _delay; i++)
            {
                var test = TestClass.StaticPublicMethod("test");
            }
            _stopWatch.Stop();
            Console.WriteLine("Static Public method directly: {0}", _stopWatch.ElapsedMilliseconds);
        }

        private static void TestStaticFields()
        {
            var sfg1 = DelegateFactory.StaticFieldGet<TestClass, string>("StaticPublicField");
            var sfg2 = DelegateFactory.StaticFieldGetExpr<TestClass, string>("StaticPublicField");
            var sfg3 = Type.StaticFieldGet<string>("StaticPublicField");
            var sfg4 = Type.StaticFieldGet<string>("StaticInternalField");
            var sfg5 = Type.StaticFieldGet<string>("StaticProtectedField");
            var sfg6 = Type.StaticFieldGet<string>("StaticPrivateField");
            var sfg7 = Type.StaticFieldGet("StaticPublicField");
            var sfg8 = Type.StaticFieldGet("StaticPublicValueField");

            var sfs1 = DelegateFactory.StaticFieldSet<TestClass, string>("StaticPublicField");
            var sfs2 = Type.StaticFieldSet<string>("StaticPublicField");
            var sfs3 = Type.StaticFieldSet("StaticPublicField");
            var sfs4 = Type.StaticFieldSet("StaticPublicReadOnlyField");

            Console.WriteLine("Static public field value: {0}", sfg1());
            sfg2();
            sfg3();
            Console.WriteLine("Static internal field value: {0}", sfg4());
            Console.WriteLine("Static protected field value: {0}", sfg5());
            Console.WriteLine("Static private field value: {0}", sfg6());
            sfg7();
            Console.WriteLine("Static public int field value: {0}", sfg8());

            sfs1("test1");
            Console.WriteLine("Static public field value: {0}", TestClass.StaticPublicField);
            sfs2("test2");
            Console.WriteLine("Static public field value: {0}", TestClass.StaticPublicField);
            sfs3("test3");
            Console.WriteLine("Static public field value: {0}", TestClass.StaticPublicField);

            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            for (var i = 0; i < 10000; i++)
            {
                DelegateFactory.StaticFieldGet<TestClass, string>("StaticPublicField");
            }
            _stopWatch.Stop();
            Console.WriteLine("Static public field creator via reflection: {0}", _stopWatch.ElapsedMilliseconds);

            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            for (var i = 0; i < 10000; i++)
            {
                DelegateFactory.StaticFieldGetExpr<TestClass, string>("StaticPublicField");
            }
            _stopWatch.Stop();
            Console.WriteLine("Static public field creator via expression: {0}", _stopWatch.ElapsedMilliseconds);

            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            for (var i = 0; i < _delay; i++)
            {
                var test = TestClass.StaticPublicField;
            }
            _stopWatch.Stop();
            Console.WriteLine("Static Public field directly: {0}", _stopWatch.ElapsedMilliseconds);

            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            for (var i = 0; i < _delay; i++)
            {
                var test = sfg1();
            }
            _stopWatch.Stop();
            Console.WriteLine("Static Public field retriever: {0}", _stopWatch.ElapsedMilliseconds);

            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            for (var i = 0; i < _delay; i++)
            {
                TestClass.StaticPublicField = "test";
            }
            _stopWatch.Stop();
            Console.WriteLine("Static public field set directly: {0}", _stopWatch.ElapsedMilliseconds);

            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            for (var i = 0; i < _delay; i++)
            {
                sfs1("test");
            }
            _stopWatch.Stop();
            Console.WriteLine("Static public field setter: {0}", _stopWatch.ElapsedMilliseconds);
        }

        private static void TestStaticProperties()
        {
            var sp1 = Type.StaticPropertyGet<string>("StaticPublicProperty");
            var sp2 = Type.StaticPropertyGet<string>("StaticInternalProperty");
            var sp3 = Type.StaticPropertyGet<string>("StaticProtectedProperty");
            var sp4 = Type.StaticPropertyGet<string>("StaticPrivateProperty");
            var spg5 = Type.StaticPropertyGet("StaticPublicProperty");
            Console.WriteLine(spg5());

            var sps = DelegateFactory.StaticPropertySet<TestClass, string>("StaticPublicProperty");
            var sps1 = Type.StaticPropertySet<string>("StaticPublicProperty");
            var sps2 = Type.StaticPropertySet<string>("StaticInternalProperty");
            var sps3 = Type.StaticPropertySet<string>("StaticProtectedProperty");
            var sps4 = Type.StaticPropertySet<string>("StaticPrivateProperty");
            var sps5 = Type.StaticPropertySet("StaticPublicProperty");
            sps5("test");
            Console.WriteLine(TestClass.StaticPublicProperty);

            var t1 = Type.StaticPropertyGet<string>("NotExistingProperty");
            var t2 = Type.StaticPropertySet<string>("NotExistingProperty");
            var t3 = Type.StaticPropertySet<string>("StaticOnlyGetProperty");
            var t4 = Type.StaticPropertyGet<string>("StaticOnlySetProperty");

            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            for (var i = 0; i < _delay; i++)
            {
                var test = TestClass.StaticPublicProperty;
            }
            _stopWatch.Stop();
            Console.WriteLine("Static Public property: {0}", _stopWatch.ElapsedMilliseconds);

            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            for (var i = 0; i < _delay; i++)
            {
                var test = sp1();
            }
            _stopWatch.Stop();
            Console.WriteLine("Static Public property retriever: {0}", _stopWatch.ElapsedMilliseconds);

            _stopWatch = new Stopwatch();
            var pi0 = Type.GetProperty("StaticPublicProperty", BindingFlags.Static | BindingFlags.Instance | BindingFlags.Public).GetMethod;
            _stopWatch.Start();
            for (var i = 0; i < _delay; i++)
            {
                var test = pi0.Invoke(null, null);
            }
            _stopWatch.Stop();
            Console.WriteLine("Static Public property via reflection: {0}", _stopWatch.ElapsedMilliseconds);

            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            for (var i = 0; i < 10000; i++)
            {
                Type.StaticPropertyGet<string>("StaticPublicProperty");
            }
            _stopWatch.Stop();
            Console.WriteLine("Static public field creator via reflection: {0}", _stopWatch.ElapsedMilliseconds);

            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            for (var i = 0; i < 10000; i++)
            {
                Type.StaticPropertyGet2<string>("StaticPublicProperty");
            }
            _stopWatch.Stop();
            Console.WriteLine("Static public field creator via expression: {0}", _stopWatch.ElapsedMilliseconds);

            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            for (var i = 0; i < _delay; i++)
            {
                TestInstance.PublicProperty = "ordinal way";
            }
            _stopWatch.Stop();
            Console.WriteLine("Public set property: {0}", _stopWatch.ElapsedMilliseconds);

            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            for (var i = 0; i < _delay; i++)
            {
                sps1("test");
            }
            _stopWatch.Stop();
            Console.WriteLine("Public set property retriever: {0}", _stopWatch.ElapsedMilliseconds);

            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            for (var i = 0; i < _delay; i++)
            {
                sps2("test");
            }
            _stopWatch.Stop();
            Console.WriteLine("Internal set property retriever: {0}", _stopWatch.ElapsedMilliseconds);

            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            for (var i = 0; i < _delay; i++)
            {
                sps3("test");
            }
            _stopWatch.Stop();
            Console.WriteLine("Protected set property retriever: {0}", _stopWatch.ElapsedMilliseconds);

            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            for (var i = 0; i < _delay; i++)
            {
                sps4("test");
            }
            _stopWatch.Stop();
            Console.WriteLine("Private set property retriever: {0}", _stopWatch.ElapsedMilliseconds);

            Console.WriteLine("Static public property value is {0}", sp1());
            Console.WriteLine("Static internal property value is {0}", sp2());
            Console.WriteLine("Static protected property value is {0}", sp3());
            Console.WriteLine("Static private property value is {0}", sp4());
        }

        private static void TestFields()
        {
            var fg1 = DelegateFactory.FieldGet<TestClass, string>("PublicField");
            var fg2 = DelegateFactory.FieldGet<TestClass, string>("InternalField");
            var fg3 = DelegateFactory.FieldGet<TestClass, string>("ProtectedField");
            var fg4 = DelegateFactory.FieldGet<TestClass, string>("_privateField");

            var fg5 = Type.FieldGet<string>("PublicField");
            var fg6 = Type.FieldGet("PublicField");
            var fg7 = Type.FieldGet2<string>("PublicField");

            Console.WriteLine("Public field value: {0}", fg1(TestInstance));
            Console.WriteLine("Internal field value: {0}", fg2(TestInstance));
            Console.WriteLine("Protected field value: {0}", fg3(TestInstance));
            Console.WriteLine("Private field value: {0}", fg4(TestInstance));
            Console.WriteLine("Public field value by object and field type: {0}", fg5(TestInstance));
            Console.WriteLine("Public field value by objects: {0}", fg6(TestInstance));

            var fs1 = DelegateFactory.FieldSetWithCast<TestClass, string>("PublicField");
            var fs2 = DelegateFactory.FieldSetWithCast<TestClass, string>("InternalField");
            var fs3 = DelegateFactory.FieldSetWithCast<TestClass, string>("ProtectedField");
            var fs4 = DelegateFactory.FieldSetWithCast<TestClass, string>("_privateField");
            var fs6 = DelegateFactory.FieldSet<TestClass, string>("PublicField");
            var fs7 = DelegateFactory.FieldSet<TestClass, string>("InternalField");
            var fs8 = DelegateFactory.FieldSet<TestClass, string>("ProtectedField");
            var fs9 = DelegateFactory.FieldSet<TestClass, string>("_privateField");
            var fs10 = Type.FieldSetWithCast<string>("_privateField");
            var fs11 = Type.FieldSet<string>("_privateField");
            var fs12 = Type.FieldSetWithCast<string>("PublicField");
            var fs13 = Type.FieldSet("PublicReadOnlyField");

            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            for (var i = 0; i < _delay; i++)
            {
                var test = TestInstance.PublicField;
            }
            _stopWatch.Stop();
            Console.WriteLine("Public field directly: {0}", _stopWatch.ElapsedMilliseconds);

            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            for (var i = 0; i < _delay; i++)
            {
                var test = fg5(TestInstance);
            }
            _stopWatch.Stop();
            Console.WriteLine("Public field via retriver with casting: {0}", _stopWatch.ElapsedMilliseconds);

            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            for (var i = 0; i < _delay; i++)
            {
                var test = fg7(TestInstance);
            }
            _stopWatch.Stop();
            Console.WriteLine("Public field via retriver without casting: {0}", _stopWatch.ElapsedMilliseconds);

            //_stopWatch = new Stopwatch();
            //_stopWatch.Start();
            //for (var i = 0; i < _delay; i++)
            //{
            //    var test = fg1(TestInstance);
            //}
            //_stopWatch.Stop();
            //Console.WriteLine("Public field retriever: {0}", _stopWatch.ElapsedMilliseconds);

            //var fieldInfo = Type.GetField("PublicField");
            //_stopWatch = new Stopwatch();
            //_stopWatch.Start();
            //for (var i = 0; i < _delay; i++)
            //{
            //    var test = fieldInfo.GetValue(TestInstance);
            //}
            //_stopWatch.Stop();
            //Console.WriteLine("Public field via reflection: {0}", _stopWatch.ElapsedMilliseconds);

            //_stopWatch = new Stopwatch();
            //_stopWatch.Start();
            //for (var i = 0; i < _delay; i++)
            //{
            //    var test = fg2(TestInstance);
            //}
            //_stopWatch.Stop();
            //Console.WriteLine("Internal field retriever: {0}", _stopWatch.ElapsedMilliseconds);

            //_stopWatch = new Stopwatch();
            //_stopWatch.Start();
            //for (var i = 0; i < _delay; i++)
            //{
            //    var test = fg3(TestInstance);
            //}
            //_stopWatch.Stop();
            //Console.WriteLine("Protected field retriever: {0}", _stopWatch.ElapsedMilliseconds);

            //_stopWatch = new Stopwatch();
            //_stopWatch.Start();
            //for (var i = 0; i < _delay; i++)
            //{
            //    var test = fg4(TestInstance);
            //}
            //_stopWatch.Stop();
            //Console.WriteLine("Private field retriever: {0}", _stopWatch.ElapsedMilliseconds);

            //_stopWatch = new Stopwatch();
            //_stopWatch.Start();
            //for (var i = 0; i < _delay; i++)
            //{
            //    var test = fg5(TestInstance);
            //}
            //_stopWatch.Stop();
            //Console.WriteLine("Public field retriever by object and field type: {0}", _stopWatch.ElapsedMilliseconds);

            //_stopWatch = new Stopwatch();
            //_stopWatch.Start();
            //for (var i = 0; i < _delay; i++)
            //{
            //    var test = fg6(TestInstance);
            //}
            //_stopWatch.Stop();
            //Console.WriteLine("Public field retriever by objects: {0}", _stopWatch.ElapsedMilliseconds);

            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            for (var i = 0; i < _delay; i++)
            {
                fs1(TestInstance, "test");
            }
            _stopWatch.Stop();
            Console.WriteLine("Public field set retriever with cast: {0}", _stopWatch.ElapsedMilliseconds);

            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            for (var i = 0; i < _delay; i++)
            {
                fs2(TestInstance, "test");
            }
            _stopWatch.Stop();
            Console.WriteLine("Internal field set retriever: {0}", _stopWatch.ElapsedMilliseconds);

            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            for (var i = 0; i < _delay; i++)
            {
                fs3(TestInstance, "test");
            }
            _stopWatch.Stop();
            Console.WriteLine("Protected field set retriever: {0}", _stopWatch.ElapsedMilliseconds);

            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            for (var i = 0; i < _delay; i++)
            {
                fs4(TestInstance, "test");
            }
            _stopWatch.Stop();
            Console.WriteLine("Private field set retriever: {0}", _stopWatch.ElapsedMilliseconds);

            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            for (var i = 0; i < _delay; i++)
            {
                fs6(TestInstance, "test");
            }
            _stopWatch.Stop();
            Console.WriteLine("Public field set retriever without cast: {0}", _stopWatch.ElapsedMilliseconds);

            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            for (var i = 0; i < _delay; i++)
            {
                fs7(TestInstance, "test");
            }
            _stopWatch.Stop();
            Console.WriteLine("Internal field set retriever without cast: {0}", _stopWatch.ElapsedMilliseconds);

            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            for (var i = 0; i < _delay; i++)
            {
                fs8(TestInstance, "test");
            }
            _stopWatch.Stop();
            Console.WriteLine("Protected field set retriever without cast: {0}", _stopWatch.ElapsedMilliseconds);

            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            for (var i = 0; i < _delay; i++)
            {
                fs9(TestInstance, "test");
            }
            _stopWatch.Stop();
            Console.WriteLine("Private field set retriever without cast: {0}", _stopWatch.ElapsedMilliseconds);

            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            for (var i = 0; i < _delay; i++)
            {
                fs10(TestInstance, "test");
            }
            _stopWatch.Stop();
            Console.WriteLine("Private field set retriever by object and field type: {0}", _stopWatch.ElapsedMilliseconds);

            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            for (var i = 0; i < _delay; i++)
            {
                fs11(TestInstance, "test");
            }
            _stopWatch.Stop();
            Console.WriteLine("Private field set retriever by object and field type without a cast: {0}", _stopWatch.ElapsedMilliseconds);
        }

        private static void TestProperties()
        {
            var pg1 = DelegateFactory.PropertyGet<TestClass, string>("PublicProperty");
            var pg2 = DelegateFactory.PropertyGet<TestClass, string>("InternalProperty");
            var pg3 = DelegateFactory.PropertyGet<TestClass, string>("ProtectedProperty");
            var pg4 = DelegateFactory.PropertyGet<TestClass, string>("PrivateProperty");

            var pgo1 = Type.PropertyGet<string>("PublicProperty");
            var pgo2 = Type.PropertyGet("PublicProperty");
            var pgo3 = Type.PropertyGet("PublicPropertyInt");
            Console.WriteLine("Public property by object and property type {0}", pgo1(TestInstance));
            Console.WriteLine("Public property by objects {0}", pgo2(TestInstance));
            Console.WriteLine("Public property by objects and with return value type {0}", pgo3(TestInstance));

            var pso1 = Type.PropertySet<string>("PublicProperty");
            var pso2 = Type.PropertySet("PublicProperty");

            pso1(TestInstance, "test");
            Console.WriteLine("Public property by object and property type {0}", TestInstance.PublicProperty);
            pso2(TestInstance, "test2");
            Console.WriteLine("Public property by objects {0}", TestInstance.PublicProperty);

            var ps1 = DelegateFactory.PropertySet<TestClass, string>("PublicProperty");
            var ps2 = DelegateFactory.PropertySet<TestClass, string>("InternalProperty");
            var ps3 = DelegateFactory.PropertySet<TestClass, string>("ProtectedProperty");
            var ps4 = DelegateFactory.PropertySet<TestClass, string>("PrivateProperty");

            Console.WriteLine("Public property value is {0}", pg1(TestInstance));
            Console.WriteLine("Internal property value is {0}", pg2(TestInstance));
            Console.WriteLine("Protected property value is {0}", pg3(TestInstance));
            Console.WriteLine("Private property value is {0}", pg4(TestInstance));

            ps1(TestInstance, "test");
            ps2(TestInstance, "test");
            ps3(TestInstance, "test");
            ps4(TestInstance, "test");

            Console.WriteLine("Public property value is {0}", pg1(TestInstance));
            Console.WriteLine("Internal property value is {0}", pg2(TestInstance));
            Console.WriteLine("Protected property value is {0}", pg3(TestInstance));
            Console.WriteLine("Private property value is {0}", pg4(TestInstance));

            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            for (var i = 0; i < _delay; i++)
            {
                var test = TestInstance.PublicProperty;
            }
            _stopWatch.Stop();
            Console.WriteLine("Public property: {0}", _stopWatch.ElapsedMilliseconds);

            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            for (var i = 0; i < _delay; i++)
            {
                var test = pg1(TestInstance);
            }
            _stopWatch.Stop();
            Console.WriteLine("Public property retriever: {0}", _stopWatch.ElapsedMilliseconds);

            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            for (var i = 0; i < _delay; i++)
            {
                var test = pg2(TestInstance);
            }
            _stopWatch.Stop();
            Console.WriteLine("Internal property retriever: {0}", _stopWatch.ElapsedMilliseconds);

            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            for (var i = 0; i < _delay; i++)
            {
                var test = TestInstance.InternalProperty;
            }
            _stopWatch.Stop();
            Console.WriteLine("Internal property: {0}", _stopWatch.ElapsedMilliseconds);

            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            for (var i = 0; i < _delay; i++)
            {
                var test = pg3(TestInstance);
            }
            _stopWatch.Stop();
            Console.WriteLine("Protected property: {0}", _stopWatch.ElapsedMilliseconds);

            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            for (var i = 0; i < _delay; i++)
            {
                var test = pg4(TestInstance);
            }
            _stopWatch.Stop();
            Console.WriteLine("Protected property: {0}", _stopWatch.ElapsedMilliseconds);

            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            for (var i = 0; i < _delay; i++)
            {
                TestInstance.PublicProperty = "test";
            }
            _stopWatch.Stop();
            Console.WriteLine("Public set property: {0}", _stopWatch.ElapsedMilliseconds);

            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            for (var i = 0; i < _delay; i++)
            {
                ps1(TestInstance, "test");
            }
            _stopWatch.Stop();
            Console.WriteLine("Public set property retriever: {0}", _stopWatch.ElapsedMilliseconds);

            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            for (var i = 0; i < _delay; i++)
            {
                ps2(TestInstance, "test");
            }
            _stopWatch.Stop();
            Console.WriteLine("Internal set property retriever: {0}", _stopWatch.ElapsedMilliseconds);

            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            for (var i = 0; i < _delay; i++)
            {
                ps3(TestInstance, "test");
            }
            _stopWatch.Stop();
            Console.WriteLine("Protected set property retriever: {0}", _stopWatch.ElapsedMilliseconds);

            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            for (var i = 0; i < _delay; i++)
            {
                ps4(TestInstance, "test");
            }
            _stopWatch.Stop();
            Console.WriteLine("Private set property retriever: {0}", _stopWatch.ElapsedMilliseconds);
        }
    }

    //class ConsoleApplication2
    //{
    //    private static readonly Type Type = typeof(TestClass);

    //    static void Main(string[] args)
    //    {
    //        var sp = DelegateFactory.StaticPropertyGet<TestClass, string>("StaticPublicProperty");
    //        var sp1 = Type.StaticPropertyGet<string>("StaticPublicProperty");
    //        var sp2 = Type.StaticPropertyGet<string>("StaticInternalProperty");
    //        var sp3 = Type.StaticPropertyGet<string>("StaticProtectedProperty");
    //        var sp4 = Type.StaticPropertyGet<string>("StaticPrivateProperty");

    //        Console.WriteLine("Static public property value: {0}", sp1());
    //        Console.WriteLine("Static internal property value: {0}", sp2());
    //        Console.WriteLine("Static protected property value: {0}", sp3());
    //        Console.WriteLine("Static private property value: {0}", sp4());
    //    }
    //}
}
