// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConsoleApplication.cs" company="Natan Podbielski">
//   Copyright (c) 2016 - 2016 Natan Podbielski. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Delegates;
using Delegates.Extensions;
using DelegatesTest;
using DelegatesTest.TestObjects;

namespace DelegatesApp
{
    internal class ConsoleApplication
    {
        private static Stopwatch _stopWatch;
        private const double Delay = 1e8;
        private static readonly TestClass TestInstance = new TestClass();
        private static readonly Type Type = typeof(TestClass);

        private static void Main(string[] args)
        {
            TestConstructors();
            TestStaticProperties();
            TestProperties();
            TestIndexers();
            TestStaticFields();
            TestFields();
            TestStaticMethods();
            TestInstanceMethods();
            TestGenerics();
            TestEvents();
        }

        private static void TestConstructors()
        {
            var cd = DelegateFactory.DefaultContructor<TestClass>();
            var cd1 = Type.DefaultContructor();
            var cd2 = Type.Contructor<Func<object>>();

            var t_ = cd();
            var t1 = cd1();
            var t2 = cd2();

            var c1 = DelegateFactory.Contructor<Func<TestClass>>();
            var c2 = DelegateFactory.Contructor<Func<int, TestClass>>();
            var c3 = DelegateFactory.Contructor<Func<bool, TestClass>>();
            var c4 = DelegateFactory.Contructor<Func<string, TestClass>>();
            var c5 = DelegateFactory.Contructor<Func<int, TestClass>>();
            var c6 = Type.Contructor(typeof(int));
            var c7 = typeof(TestStruct).Contructor<Func<int, object>>();
            var c8 = typeof(TestStruct).Contructor(typeof(int));

            var t3 = c1();
            var t4 = c2(0);
            var t5 = c3(false);
            var t6 = c4("");
            var t7 = c5(0);
            var t8 = c6(new object[] { 0 });
            var t9 = c7(0);
            var t10 = c8(new object[] { 0 });

            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            for (var i = 0; i < Delay; i++)
            {
                var test = new TestClass();
            }
            _stopWatch.Stop();
            Console.WriteLine($"Public default constructor directly: {_stopWatch.ElapsedMilliseconds}");

            var constructorInfo = Type.GetConstructor(Type.EmptyTypes);
            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            for (var i = 0; i < Delay; i++)
            {
                var test = (TestClass)constructorInfo.Invoke(null);
            }
            _stopWatch.Stop();
            Console.WriteLine($"Public default constructor via reflection: {_stopWatch.ElapsedMilliseconds}");

            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            for (var i = 0; i < Delay; i++)
            {
                var test = Activator.CreateInstance<TestClass>();
            }
            _stopWatch.Stop();
            Console.WriteLine($"Public default constructor via Activator: {_stopWatch.ElapsedMilliseconds}");


            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            for (var i = 0; i < Delay; i++)
            {
                var t = c1();
            }
            _stopWatch.Stop();
            Console.WriteLine($"Public default constructor proxy: {_stopWatch.ElapsedMilliseconds}");

            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            for (var i = 0; i < Delay; i++)
            {
                var test = c5(0);
            }
            _stopWatch.Stop();
            Console.WriteLine($"Public constructor with parameter proxy: {_stopWatch.ElapsedMilliseconds}");

            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            for (var i = 0; i < Delay; i++)
            {
                var test = c6(new object[] { 0 });
            }
            _stopWatch.Stop();
            Console.WriteLine($"Public constructor with parameter via array proxy: {_stopWatch.ElapsedMilliseconds}");

            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            for (var i = 0; i < Delay; i++)
            {
                var t = c2(0);
            }
            _stopWatch.Stop();
            Console.WriteLine($"Internal constructor proxy: {_stopWatch.ElapsedMilliseconds}");

            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            for (var i = 0; i < Delay; i++)
            {
                var t = c3(false);
            }
            _stopWatch.Stop();
            Console.WriteLine($"Protected constructor proxy: {_stopWatch.ElapsedMilliseconds}");

            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            for (var i = 0; i < Delay; i++)
            {
                var t = c4("");
            }
            _stopWatch.Stop();
            Console.WriteLine($"Private constructor proxy: {_stopWatch.ElapsedMilliseconds}");
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
#if !NET35
            var is4 = Type.IndexerSet(typeof(int), typeof(int), typeof(int), typeof(int));
#endif
            var is5 = Type.IndexerSet<int, int>();
            var is6 = Type.IndexerSet<int, int, int>();
            var is7 = Type.IndexerSet<int, int, int, int>();

            is1(TestInstance, 0, 1);
            Console.WriteLine(TestInstance[0]);
            is2(TestInstance, 0, 0, 1);
            is3(TestInstance, 0, 0, 0, 1);
#if !NET35
            is4(TestInstance, new object[] { 0, 0, 0 }, 1);
#endif
            is5(TestInstance, 1, 2);
            Console.WriteLine(TestInstance[1]);
            is6(TestInstance, 0, 0, 2);
            is7(TestInstance, 0, 0, 0, 2);


            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            for (var i = 0; i < Delay; i++)
            {
                var test = TestInstance[0];
            }
            _stopWatch.Stop();
            Console.WriteLine($"Public indexer: {_stopWatch.ElapsedMilliseconds}");

            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            for (var i = 0; i < Delay; i++)
            {
                var test = ig1(TestInstance, 0);
            }
            _stopWatch.Stop();
            Console.WriteLine($"Public indexer retriever: {_stopWatch.ElapsedMilliseconds}");

            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            for (var i = 0; i < Delay; i++)
            {
                var test = ig2(TestInstance, "s");
            }
            _stopWatch.Stop();
            Console.WriteLine($"Internal indexer retriever: {_stopWatch.ElapsedMilliseconds}");

            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            for (var i = 0; i < Delay; i++)
            {
                var test = ig3(TestInstance, i);
            }
            _stopWatch.Stop();
            Console.WriteLine($"Private indexer: {_stopWatch.ElapsedMilliseconds}");

            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            for (var i = 0; i < Delay; i++)
            {
                var test = TestInstance[0, 0, 0];
            }
            _stopWatch.Stop();
            Console.WriteLine($"Multiple index indexer directly: {_stopWatch.ElapsedMilliseconds}");

            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            for (var i = 0; i < Delay; i++)
            {
                var test = ig10(TestInstance, new object[] { 0, 0, 0 });
            }
            _stopWatch.Stop();
            Console.WriteLine($"Multiple index indexer via delegate with array: {_stopWatch.ElapsedMilliseconds}");

            var indexerInfo = Type.GetProperty("TheItem", typeof(int), new[] { typeof(int), typeof(int), typeof(int) });
            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            for (var i = 0; i < Delay; i++)
            {
                var test = indexerInfo.GetValue(TestInstance, new object[] { 0, 0, 0 });
            }
            _stopWatch.Stop();
            Console.WriteLine($"Multiple index indexer via reflection: {_stopWatch.ElapsedMilliseconds}");
        }

        private static void TestEvents()
        {
            var ea1 = DelegateFactory.EventAdd<TestClass, TestClass.PublicEventArgs>("PublicEvent");
            var ea2 = DelegateFactory.EventAdd<TestClass, TestClass.InternalEventArgs>("InternalEvent");
            var ea3 = DelegateFactory.EventAdd<TestClass>("ProtectedEvent");
            var ea4 = Type.EventAdd<TestClass.PublicEventArgs>("PublicEvent");
            var ea5 = Type.EventAdd("PrivateEvent");

            var er1 = DelegateFactory.EventRemove<TestClass, TestClass.PublicEventArgs>("PublicEvent");
            var er2 = DelegateFactory.EventRemove<TestClass, TestClass.InternalEventArgs>("InternalEvent");
            var er3 = DelegateFactory.EventRemove<TestClass>("ProtectedEvent");
            var er4 = Type.EventRemove<TestClass.PublicEventArgs>("PublicEvent");
            var er5 = Type.EventRemove("PrivateEvent");

            ea1(TestInstance, TypelessHandler);
            ea2(TestInstance, TypelessHandler);
            ea3(TestInstance, HandlerWithSourceType);
            ea4(TestInstance, HandlerWithoutSourceType);
            ea5(TestInstance, TypelessHandler);

            TestInstance.InvokePublicEvent();
            TestInstance.InvokeInternalEvent();
            TestInstance.InvokeProtectedEvent();
            TestInstance.InvokePrivateEvent();

            er1(TestInstance, TypelessHandler);
            er2(TestInstance, TypelessHandler);
            er3(TestInstance, HandlerWithSourceType);
            er4(TestInstance, HandlerWithoutSourceType);
            er5(TestInstance, TypelessHandler);

            TestInstance.InvokePublicEvent();
            TestInstance.InvokeInternalEvent();
            TestInstance.InvokeProtectedEvent();
            TestInstance.InvokePrivateEvent();
        }

        private static void HandlerWithoutSourceType(object o, TestClass.PublicEventArgs eventArgs)
        {
            Console.WriteLine("Public handler without source type works!");
        }

        private static void HandlerWithSourceType(TestClass sender, object eventArgs)
        {
            if (eventArgs.GetType() ==
                Type.GetNestedType("ProtectedEventArgs", BindingFlags.Instance | BindingFlags.NonPublic))
            {
                Console.WriteLine("Protected handler works!");
            }
        }

        private static void TypelessHandler(object sender, object eventArgs)
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
                     Type.GetNestedType("PrivateEventArgs", BindingFlags.Instance | BindingFlags.NonPublic))
            {
                Console.WriteLine("Private handler works!");
            }
        }

        private static void TestGenerics()
        {
            var g1 =
                DelegateFactory.StaticMethod<TestClass, Func<TestClass, TestClass>, TestClass>("StaticGenericMethod");
            var g2 =
                DelegateFactory.StaticMethod<TestClass, Func<TestClass, int, TestClass>, TestClass>(
                    "StaticGenericMethod");
            var g3 =
                DelegateFactory.StaticMethod<TestClass, Func<TestStruct, int, bool, TestStruct>, TestStruct>(
                    "StaticGenericMethod");
            var g4 = DelegateFactory.StaticMethod<TestClass, Func<TestClass>, TestClass>("StaticGenericMethod");
            var g5 =
                DelegateFactory.StaticMethod<TestClass, Func<TestClass>, TestClass, TestStruct>("StaticGenericMethod");
            var g6 =
                DelegateFactory.StaticMethod<TestClass, Func<int, TestClass>, TestClass, TestStruct, int>(
                    "StaticGenericMethod");
            var g7 = DelegateFactory.StaticMethod<TestClass, Func<int, TestClass>>("StaticGenericMethod",
                typeof(TestClass), typeof(TestStruct), typeof(int));

            var g8 = Type.StaticMethod<Func<TestClass, TestClass>, TestClass>("StaticGenericMethod");
            var g9 = Type.StaticMethod<Func<TestClass, int, TestClass>, TestClass>("StaticGenericMethod");
            var g10 = Type.StaticMethod<Func<TestStruct, int, bool, TestStruct>, TestStruct>("StaticGenericMethod");
            var g11 = Type.StaticMethod<Func<TestClass>, TestClass>("StaticGenericMethod");
            var g12 = Type.StaticMethod<Func<TestClass>, TestClass, TestStruct>("StaticGenericMethod");
            var g13 = Type.StaticMethod<Func<int, TestClass>, TestClass, TestStruct, int>("StaticGenericMethod");
            var g14 = Type.StaticMethod<Func<int, TestClass>>("StaticGenericMethod", typeof(TestClass),
                typeof(TestStruct), typeof(int));

            var g15 = Type.StaticGenericMethod("StaticGenericMethod", new[] { Type }, new[] { Type });
            var g16 = Type.StaticGenericMethod("StaticGenericMethod", new[] { Type, typeof(int) }, new[] { Type });
            var g17 = Type.StaticGenericMethod("StaticGenericMethod",
                new[] { typeof(TestStruct), typeof(int), typeof(bool) }, new[] { typeof(TestStruct) });
            var g18 = Type.StaticGenericMethod("StaticGenericMethod", Type.EmptyTypes, new[] { Type });
            var g19 = Type.StaticGenericMethod("StaticGenericMethod", Type.EmptyTypes, new[] { Type, typeof(TestStruct) });
            var g20 = Type.StaticGenericMethod("StaticGenericMethod", new[] { typeof(int) },
                new[] { Type, typeof(TestStruct), typeof(int) });
            var g21 = Type.StaticGenericMethodVoid("StaticGenericMethodVoid", new[] { Type }, new[] { Type });

            var t = g1(TestInstance);
            var t2 = g2(TestInstance, 0);
            var t3 = g3(new TestStruct(), 0, false);
            var t4 = g4();
            var t5 = g5();
            var t6 = g6(0);
            var t7 = g7(0);

            var t8 = g8(TestInstance);
            var t9 = g9(TestInstance, 0);
            var t10 = g10(new TestStruct(), 0, false);
            var t11 = g11();
            var t12 = g12();
            var t13 = g13(0);
            var t14 = g14(0);

            var t15 = g15(new object[] { TestInstance });
            var t16 = g16(new object[] { TestInstance, 0 });
            var t17 = g17(new object[] { new TestStruct(), 0, false });
            var t18 = g18(new object[] { });
            var t19 = g19(new object[] { });
            var t20 = g20(new object[] { 0 });
            g21(new object[] { TestInstance });
            var t21 = TestClass.StaticGenericMethodVoidParameter;

            var g22 = Type.StaticGenericMethodVoid("StaticGenericMethodVoid", new[] { typeof(object) },
                new[] { typeof(object) });
            g22(new object[] { "" });
            var t22 = TestClass.StaticGenericMethodVoidParameter;
            g22(new object[] { TestInstance });
            var t23 = TestClass.StaticGenericMethodVoidParameter;

            var ig1 = DelegateFactory.InstanceMethod<Func<TestClass, TestClass, TestClass>, TestClass>("GenericMethod");
            var ig2 = Type.InstanceMethod<Func<TestClass, TestClass, TestClass>, TestClass>("GenericMethod");
            var ig3 = Type.InstanceMethod<Func<object, TestClass, TestClass>, TestClass>("GenericMethod");
            var ig4 = Type.InstanceGenericMethod("GenericMethod", new[] { Type }, new[] { Type });
            var ig5 = Type.InstanceGenericMethodVoid("GenericMethodVoid", new[] { Type }, new[] { Type });

            var it1 = ig1(TestInstance, TestInstance);
            var it2 = ig2(TestInstance, TestInstance);
            var it3 = ig3(TestInstance, TestInstance);
            var it4 = ig4(TestInstance, new object[] { TestInstance });
            ig5(TestInstance, new object[] { TestInstance });
            var it5 = TestInstance.InstanceGenericMethodVoidParameter;

            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            for (var i = 0; i < Delay; i++)
            {
                var test = TestClass.StaticGenericMethod(TestInstance);
            }
            _stopWatch.Stop();
            Console.WriteLine($"Static generic method directly: {_stopWatch.ElapsedMilliseconds}");

            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            for (var i = 0; i < Delay; i++)
            {
                var test = g1(TestInstance);
            }
            _stopWatch.Stop();
            Console.WriteLine($"Static generic method proxy: {_stopWatch.ElapsedMilliseconds}");

            var methodInfos =
                Type.GetMethods(BindingFlags.Static | BindingFlags.Public)
                    .Where(
                        m =>
                            m.Name == "StaticGenericMethod" && m.IsGenericMethod && m.GetParameters().Length == 1 &&
                            m.GetGenericArguments().Length == 1);
            var methodInfo = methodInfos.Single();
            methodInfo = methodInfo.MakeGenericMethod(Type);

            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            for (var i = 0; i < Delay; i++)
            {
                var test = methodInfo.Invoke(null, new object[] { TestInstance });
            }
            _stopWatch.Stop();
            Console.WriteLine($"Static generic method via reflection: {_stopWatch.ElapsedMilliseconds}");
        }

        private static void TestInstanceMethods()
        {
            var m1 = DelegateFactory.InstanceMethod<Func<TestClass, string, string>>("PublicMethod");
            var m2 = DelegateFactory.InstanceMethod<Func<TestClass, string, string>>("InternalMethod");
            var m3 = DelegateFactory.InstanceMethod<Func<TestClass, string, string>>("ProtectedMethod");
            var m4 = DelegateFactory.InstanceMethod<Func<TestClass, string, string>>("PrivateMethod");
#if !NET35
            var m5 = DelegateFactory.InstanceMethodExpr<Func<TestClass, string, string>>("PublicMethod");
#endif
            var m6 = Type.InstanceMethod<Func<TestClass, string, string>>("PublicMethod");
            var m7 = Type.InstanceMethod<Func<object, string, string>>("PublicMethod");
            var m8 = Type.InstanceMethod("PublicMethod", typeof(string));
            var m9 = Type.InstanceMethodVoid("PublicMethodVoid", typeof(string));

            var t = m1(TestInstance, "test");
            var t2 = m2(TestInstance, "test");
            var t3 = m3(TestInstance, "test");
            var t4 = m4(TestInstance, "test");
#if !NET35
            var t5 = m5(TestInstance, "test");
#endif
            var t6 = m6(TestInstance, "test");
            var t7 = m7(TestInstance, "test");
            var t8 = m8(TestInstance, new object[] { "test" });
            m9(TestInstance, new object[] { "test" });
            var t9 = TestInstance.PublicMethodVoidParameter;

            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            for (var i = 0; i < Delay; i++)
            {
                var test = TestInstance.PublicMethod("test");
            }
            _stopWatch.Stop();
            Console.WriteLine($"Public method directly: {_stopWatch.ElapsedMilliseconds}");

            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            for (var i = 0; i < Delay; i++)
            {
                var test = m1(TestInstance, "test");
            }
            _stopWatch.Stop();
            Console.WriteLine($"Public method proxy: {_stopWatch.ElapsedMilliseconds}");

            var methodInfo = Type.GetMethod("PublicMethod");
            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            for (var i = 0; i < Delay; i++)
            {
                var test = methodInfo.Invoke(TestInstance, new object[] { "test" });
            }
            _stopWatch.Stop();
            Console.WriteLine($"Public method proxy: {_stopWatch.ElapsedMilliseconds}");

            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            for (var i = 0; i < 10000; i++)
            {
                DelegateFactory.InstanceMethod<Func<TestClass, string, string>>("PrivateMethod");
            }
            _stopWatch.Stop();
            Console.WriteLine($"Private method proxy creator via reflection: {_stopWatch.ElapsedMilliseconds}");

#if !NET35
            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            for (var i = 0; i < 10000; i++)
            {
                DelegateFactory.InstanceMethodExpr<Func<TestClass, string, string>>("PrivateMethod");
            }
            _stopWatch.Stop();
            Console.WriteLine($"Private method proxy creator via expression: {_stopWatch.ElapsedMilliseconds}");
#endif
        }

        private static void TestStaticMethods()
        {
            var sm1 = DelegateFactory.StaticMethod<TestClass, Func<string, string>>("StaticPublicMethod");
            var sm2 = DelegateFactory.StaticMethod<TestClass, Func<string, string>>("StaticInternalMethod");
            var sm3 = DelegateFactory.StaticMethod<TestClass, Func<string, string>>("StaticProtectedMethod");
            var sm4 = DelegateFactory.StaticMethod<TestClass, Func<string, string>>("StaticPrivateMethod");
            var sm5 = Type.StaticMethod<Func<string, string>>("StaticPublicMethod");
            var sm6 = Type.StaticMethod<Func<string, string>>("StaticInternalMethod");
            var sm7 = Type.StaticMethod<Func<string, string>>("StaticProtectedMethod");
            var sm8 = Type.StaticMethod<Func<string, string>>("StaticPrivateMethod");
            var sm9 = Type.StaticMethod("StaticPublicMethod", typeof(string));
            var sm10 = Type.StaticMethodVoid("StaticPublicMethodVoid", typeof(string));
            var sm11 = Type.StaticMethod("StaticInternalMethod", typeof(string));
            var sm12 = Type.StaticMethod("StaticProtectedMethod", typeof(string));
            var sm13 = Type.StaticMethod("StaticPrivateMethod", typeof(string));
            var sm14 = DelegateFactory.StaticMethod<TestClass, Func<int, int>>("StaticPublicMethodValue");
            var sm15 = Type.StaticMethod<Func<int, int>>("StaticPublicMethodValue");
            var sm16 = Type.StaticMethod("StaticPublicMethodValue", typeof(int));

            var t = sm1("test");
            var t2 = sm2("test");
            var t3 = sm3("test");
            var t4 = sm4("test");
            var t5 = sm5("test");
            var t6 = sm6("test");
            var t7 = sm7("test");
            var t8 = sm8("test");
            var t9 = sm9(new object[] { "test" });
            sm10(new object[] { "test" });
            var t10 = TestClass.StaticPublicMethodVoidParameter;
            var t11 = sm11(new object[] { "test" });
            var t12 = sm12(new object[] { "test" });
            var t13 = sm13(new object[] { "test" });
            var t14 = sm14(0);
            var t15 = sm15(0);
            var t16 = sm16(new object[] { 0 });

            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            for (var i = 0; i < Delay; i++)
            {
                var test = TestClass.StaticPublicMethod("test");
            }
            _stopWatch.Stop();
            Console.WriteLine($"Static Public method directly: {_stopWatch.ElapsedMilliseconds}");

            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            for (var i = 0; i < Delay; i++)
            {
                var test = sm1("test");
            }
            _stopWatch.Stop();
            Console.WriteLine($"Static Public method proxy: {_stopWatch.ElapsedMilliseconds}");

            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            for (var i = 0; i < Delay; i++)
            {
                var test = sm9(new object[] { "test" });
            }
            _stopWatch.Stop();
            Console.WriteLine($"Static Public method via proxy with array: {_stopWatch.ElapsedMilliseconds}");

            var methodInfo = Type.GetMethod("StaticPublicMethod", new[] { typeof(string) });
            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            for (var i = 0; i < Delay; i++)
            {
                var test = methodInfo.Invoke(null, new object[] { "test" });
            }
            _stopWatch.Stop();
            Console.WriteLine($"Static Public method via reflection: {_stopWatch.ElapsedMilliseconds}");

            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            for (var i = 0; i < Delay; i++)
            {
                var test = sm2("test");
            }
            _stopWatch.Stop();
            Console.WriteLine($"Static internal method proxy: {_stopWatch.ElapsedMilliseconds}");

            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            for (var i = 0; i < Delay; i++)
            {
                var test = sm3("test");
            }
            _stopWatch.Stop();
            Console.WriteLine($"Static protected method proxy: {_stopWatch.ElapsedMilliseconds}");

            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            for (var i = 0; i < Delay; i++)
            {
                var test = sm4("test");
            }
            _stopWatch.Stop();
            Console.WriteLine($"Static private method proxy: {_stopWatch.ElapsedMilliseconds}");
        }

        private static void TestStaticFields()
        {
            var sfg1 = DelegateFactory.StaticFieldGet<TestClass, string>("StaticPublicField");
#if !NET35
            var sfg2 = DelegateFactory.StaticFieldGetExpr<TestClass, string>("StaticPublicField");
#endif
            var sfg3 = Type.StaticFieldGet<string>("StaticPublicField");
            var sfg4 = Type.StaticFieldGet<string>("StaticInternalField");
            var sfg5 = Type.StaticFieldGet<string>("StaticProtectedField");
            var sfg6 = Type.StaticFieldGet<string>("StaticPrivateField");
            var sfg7 = Type.StaticFieldGet("StaticPublicField");
            var sfg8 = Type.StaticFieldGet("StaticPublicValueField");

#if !NET35
            var sfs1 = DelegateFactory.StaticFieldSet<TestClass, string>("StaticPublicField");
            var sfs2 = Type.StaticFieldSet<string>("StaticPublicField");
            var sfs3 = Type.StaticFieldSet("StaticPublicField");
            var sfs4 = Type.StaticFieldSet("StaticPublicReadOnlyField");
#endif

            Console.WriteLine($"Static public field value: {sfg1()}");
#if !NET35
            sfg2();
#endif
            sfg3();
            Console.WriteLine($"Static internal field value: {sfg4()}");
            Console.WriteLine($"Static protected field value: {sfg5()}");
            Console.WriteLine($"Static private field value: {sfg6()}");
            sfg7();
            Console.WriteLine($"Static public int field value: {sfg8()}");

#if !NET35
            sfs1("test1");
            Console.WriteLine($"Static public field value: {TestClass.StaticPublicField}");
            sfs2("test2");
            Console.WriteLine($"Static public field value: {TestClass.StaticPublicField}");
            sfs3("test3");
            Console.WriteLine($"Static public field value: {TestClass.StaticPublicField}");
#endif

            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            for (var i = 0; i < 10000; i++)
            {
                DelegateFactory.StaticFieldGet<TestClass, string>("StaticPublicField");
            }
            _stopWatch.Stop();
            Console.WriteLine($"Static public field creator via reflection: {_stopWatch.ElapsedMilliseconds}");

#if !NET35
            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            for (var i = 0; i < 10000; i++)
            {
                DelegateFactory.StaticFieldGetExpr<TestClass, string>("StaticPublicField");
            }
            _stopWatch.Stop();
            Console.WriteLine($"Static public field creator via expression: {_stopWatch.ElapsedMilliseconds}");
#endif

            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            for (var i = 0; i < Delay; i++)
            {
                var test = TestClass.StaticPublicField;
            }
            _stopWatch.Stop();
            Console.WriteLine($"Static Public field directly: {_stopWatch.ElapsedMilliseconds}");

            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            for (var i = 0; i < Delay; i++)
            {
                var test = sfg1();
            }
            _stopWatch.Stop();
            Console.WriteLine($"Static Public field retriever: {_stopWatch.ElapsedMilliseconds}");

            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            for (var i = 0; i < Delay; i++)
            {
                TestClass.StaticPublicField = "test";
            }
            _stopWatch.Stop();
            Console.WriteLine($"Static public field set directly: {_stopWatch.ElapsedMilliseconds}");

#if !NET35
            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            for (var i = 0; i < Delay; i++)
            {
                sfs1("test");
            }
            _stopWatch.Stop();
            Console.WriteLine($"Static public field setter: {_stopWatch.ElapsedMilliseconds}");
#endif
        }

        private static void TestStaticProperties()
        {
            var sp1 = Type.StaticPropertyGet<string>("StaticPublicProperty");
            var sp2 = Type.StaticPropertyGet<string>("StaticInternalProperty");
            var sp3 = Type.StaticPropertyGet<string>("StaticProtectedProperty");
            var sp4 = Type.StaticPropertyGet<string>("StaticPrivateProperty");
            var spg5 = Type.StaticPropertyGet("StaticPublicProperty");
            var spg6 = DelegateFactory.StaticPropertyGet<TestClass, string>("StaticPublicProperty");
            Console.WriteLine(spg5());
            spg6();

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
            for (var i = 0; i < Delay; i++)
            {
                var test = TestClass.StaticPublicProperty;
            }
            _stopWatch.Stop();
            Console.WriteLine($"Static Public property: {_stopWatch.ElapsedMilliseconds}");

            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            for (var i = 0; i < Delay; i++)
            {
                var test = sp1();
            }
            _stopWatch.Stop();
            Console.WriteLine($"Static Public property retriever: {_stopWatch.ElapsedMilliseconds}");

            _stopWatch = new Stopwatch();
            var pi0 =
#if NET35 || NET4
                new CPropertyInfo( 
#endif
                    Type.GetProperty("StaticPublicProperty",
                    BindingFlags.Static | BindingFlags.Instance | BindingFlags.Public)
#if NET35 || NET4
                    )
#endif
                    .GetMethod;
            _stopWatch.Start();
            for (var i = 0; i < Delay; i++)
            {
                var test = pi0.Invoke(null, null);
            }
            _stopWatch.Stop();
            Console.WriteLine($"Static Public property via reflection: {_stopWatch.ElapsedMilliseconds}");

            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            for (var i = 0; i < 10000; i++)
            {
                Type.StaticPropertyGet<string>("StaticPublicProperty");
            }
            _stopWatch.Stop();
            Console.WriteLine($"Static public field creator via reflection: {_stopWatch.ElapsedMilliseconds}");

#if !NET35
            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            for (var i = 0; i < 10000; i++)
            {
                Type.StaticPropertyGetExpr<string>("StaticPublicProperty");
            }
            _stopWatch.Stop();
            Console.WriteLine($"Static public field creator via expression: {_stopWatch.ElapsedMilliseconds}");
#endif

            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            for (var i = 0; i < Delay; i++)
            {
                TestInstance.PublicProperty = "ordinal way";
            }
            _stopWatch.Stop();
            Console.WriteLine($"Public set property: {_stopWatch.ElapsedMilliseconds}");

            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            for (var i = 0; i < Delay; i++)
            {
                sps1("test");
            }
            _stopWatch.Stop();
            Console.WriteLine($"Public set property retriever: {_stopWatch.ElapsedMilliseconds}");

            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            for (var i = 0; i < Delay; i++)
            {
                sps2("test");
            }
            _stopWatch.Stop();
            Console.WriteLine($"Internal set property retriever: {_stopWatch.ElapsedMilliseconds}");

            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            for (var i = 0; i < Delay; i++)
            {
                sps3("test");
            }
            _stopWatch.Stop();
            Console.WriteLine($"Protected set property retriever: {_stopWatch.ElapsedMilliseconds}");

            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            for (var i = 0; i < Delay; i++)
            {
                sps4("test");
            }
            _stopWatch.Stop();
            Console.WriteLine($"Private set property retriever: {_stopWatch.ElapsedMilliseconds}");

            Console.WriteLine($"Static public property value is {sp1()}");
            Console.WriteLine($"Static internal property value is {sp2()}");
            Console.WriteLine($"Static protected property value is {sp3()}");
            Console.WriteLine($"Static private property value is {sp4()}");
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

            Console.WriteLine($"Public field value: {fg1(TestInstance)}");
            Console.WriteLine($"Internal field value: {fg2(TestInstance)}");
            Console.WriteLine($"Protected field value: {fg3(TestInstance)}");
            Console.WriteLine($"Private field value: {fg4(TestInstance)}");
            Console.WriteLine($"Public field value by object and field type: {fg5(TestInstance)}");
            Console.WriteLine($"Public field value by objects: {fg6(TestInstance)}");

#if !NET35
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
#endif

            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            for (var i = 0; i < Delay; i++)
            {
                var test = TestInstance.PublicField;
            }
            _stopWatch.Stop();
            Console.WriteLine($"Public field directly: {_stopWatch.ElapsedMilliseconds}");

            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            for (var i = 0; i < Delay; i++)
            {
                var test = fg5(TestInstance);
            }
            _stopWatch.Stop();
            Console.WriteLine($"Public field via retriver with casting: {_stopWatch.ElapsedMilliseconds}");

            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            for (var i = 0; i < Delay; i++)
            {
                var test = fg7(TestInstance);
            }
            _stopWatch.Stop();
            Console.WriteLine($"Public field via retriver without casting: {_stopWatch.ElapsedMilliseconds}");

            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            for (var i = 0; i < Delay; i++)
            {
                var test = fg1(TestInstance);
            }
            _stopWatch.Stop();
            Console.WriteLine($"Public field retriever: {_stopWatch.ElapsedMilliseconds}");

            var fieldInfo = Type.GetField("PublicField");
            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            for (var i = 0; i < Delay; i++)
            {
                var test = fieldInfo.GetValue(TestInstance);
            }
            _stopWatch.Stop();
            Console.WriteLine($"Public field via reflection: {_stopWatch.ElapsedMilliseconds}");

            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            for (var i = 0; i < Delay; i++)
            {
                var test = fg2(TestInstance);
            }
            _stopWatch.Stop();
            Console.WriteLine($"Internal field retriever: {_stopWatch.ElapsedMilliseconds}");

            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            for (var i = 0; i < Delay; i++)
            {
                var test = fg3(TestInstance);
            }
            _stopWatch.Stop();
            Console.WriteLine($"Protected field retriever: {_stopWatch.ElapsedMilliseconds}");

            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            for (var i = 0; i < Delay; i++)
            {
                var test = fg4(TestInstance);
            }
            _stopWatch.Stop();
            Console.WriteLine($"Private field retriever: {_stopWatch.ElapsedMilliseconds}");

            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            for (var i = 0; i < Delay; i++)
            {
                var test = fg5(TestInstance);
            }
            _stopWatch.Stop();
            Console.WriteLine($"Public field retriever by object and field type: {_stopWatch.ElapsedMilliseconds}");

            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            for (var i = 0; i < Delay; i++)
            {
                var test = fg6(TestInstance);
            }
            _stopWatch.Stop();
            Console.WriteLine($"Public field retriever by objects: {_stopWatch.ElapsedMilliseconds}");

#if !NET35
            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            for (var i = 0; i < Delay; i++)
            {
                fs1(TestInstance, "test");
            }
            _stopWatch.Stop();
            Console.WriteLine($"Public field set retriever with cast: {_stopWatch.ElapsedMilliseconds}");

            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            for (var i = 0; i < Delay; i++)
            {
                fs2(TestInstance, "test");
            }
            _stopWatch.Stop();
            Console.WriteLine($"Internal field set retriever: {_stopWatch.ElapsedMilliseconds}");

            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            for (var i = 0; i < Delay; i++)
            {
                fs3(TestInstance, "test");
            }
            _stopWatch.Stop();
            Console.WriteLine($"Protected field set retriever: {_stopWatch.ElapsedMilliseconds}");

            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            for (var i = 0; i < Delay; i++)
            {
                fs4(TestInstance, "test");
            }
            _stopWatch.Stop();
            Console.WriteLine($"Private field set retriever: {_stopWatch.ElapsedMilliseconds}");

            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            for (var i = 0; i < Delay; i++)
            {
                fs6(TestInstance, "test");
            }
            _stopWatch.Stop();
            Console.WriteLine($"Public field set retriever without cast: {_stopWatch.ElapsedMilliseconds}");

            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            for (var i = 0; i < Delay; i++)
            {
                fs7(TestInstance, "test");
            }
            _stopWatch.Stop();
            Console.WriteLine($"Internal field set retriever without cast: {_stopWatch.ElapsedMilliseconds}");

            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            for (var i = 0; i < Delay; i++)
            {
                fs8(TestInstance, "test");
            }
            _stopWatch.Stop();
            Console.WriteLine($"Protected field set retriever without cast: {_stopWatch.ElapsedMilliseconds}");

            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            for (var i = 0; i < Delay; i++)
            {
                fs9(TestInstance, "test");
            }
            _stopWatch.Stop();
            Console.WriteLine($"Private field set retriever without cast: {_stopWatch.ElapsedMilliseconds}");

            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            for (var i = 0; i < Delay; i++)
            {
                fs10(TestInstance, "test");
            }
            _stopWatch.Stop();
            Console.WriteLine($"Private field set retriever by object and field type: {_stopWatch.ElapsedMilliseconds}");

            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            for (var i = 0; i < Delay; i++)
            {
                fs11(TestInstance, "test");
            }
            _stopWatch.Stop();
            Console.WriteLine(
                $"Private field set retriever by object and field type without a cast: {_stopWatch.ElapsedMilliseconds}"); /**/
#endif
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
            Console.WriteLine($"Public property by object and property type {pgo1(TestInstance)}");
            Console.WriteLine($"Public property by objects {pgo2(TestInstance)}");
            Console.WriteLine($"Public property by objects and with return value type {pgo3(TestInstance)}");

            var pso1 = Type.PropertySet<string>("PublicProperty");
            var pso2 = Type.PropertySet("PublicProperty");

            pso1(TestInstance, "test");
            Console.WriteLine($"Public property by object and property type {TestInstance.PublicProperty}");
            pso2(TestInstance, "test2");
            Console.WriteLine($"Public property by objects {TestInstance.PublicProperty}");

            var ps1 = DelegateFactory.PropertySet<TestClass, string>("PublicProperty");
            var ps2 = DelegateFactory.PropertySet<TestClass, string>("InternalProperty");
            var ps3 = DelegateFactory.PropertySet<TestClass, string>("ProtectedProperty");
            var ps4 = DelegateFactory.PropertySet<TestClass, string>("PrivateProperty");
            var ps5 = Type.PropertySet<string>("PublicProperty");
            var ps6 = Type.PropertySet("PublicProperty");

            Console.WriteLine($"Public property value is {pg1(TestInstance)}");
            Console.WriteLine($"Internal property value is {pg2(TestInstance)}");
            Console.WriteLine($"Protected property value is {pg3(TestInstance)}");
            Console.WriteLine($"Private property value is {pg4(TestInstance)}");

            ps1(TestInstance, "test");
            ps2(TestInstance, "test");
            ps3(TestInstance, "test");
            ps4(TestInstance, "test");
            ps5(TestInstance, "test1");
            ps6(TestInstance, "test2");

            Console.WriteLine($"Public property value is {pg1(TestInstance)}");
            Console.WriteLine($"Internal property value is {pg2(TestInstance)}");
            Console.WriteLine($"Protected property value is {pg3(TestInstance)}");
            Console.WriteLine($"Private property value is {pg4(TestInstance)}");

            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            for (var i = 0; i < Delay; i++)
            {
                var test = TestInstance.PublicProperty;
            }
            _stopWatch.Stop();
            Console.WriteLine($"Public property: {_stopWatch.ElapsedMilliseconds}");

            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            for (var i = 0; i < Delay; i++)
            {
                var test = pg1(TestInstance);
            }
            _stopWatch.Stop();
            Console.WriteLine($"Public property retriever: {_stopWatch.ElapsedMilliseconds}");

            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            for (var i = 0; i < Delay; i++)
            {
                var test = pg2(TestInstance);
            }
            _stopWatch.Stop();
            Console.WriteLine($"Internal property retriever: {_stopWatch.ElapsedMilliseconds}");

            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            for (var i = 0; i < Delay; i++)
            {
                var test = TestInstance.InternalProperty;
            }
            _stopWatch.Stop();
            Console.WriteLine($"Internal property: {_stopWatch.ElapsedMilliseconds}");

            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            for (var i = 0; i < Delay; i++)
            {
                var test = pg3(TestInstance);
            }
            _stopWatch.Stop();
            Console.WriteLine($"Protected property: {_stopWatch.ElapsedMilliseconds}");

            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            for (var i = 0; i < Delay; i++)
            {
                var test = pg4(TestInstance);
            }
            _stopWatch.Stop();
            Console.WriteLine($"Protected property: {_stopWatch.ElapsedMilliseconds}");

            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            for (var i = 0; i < Delay; i++)
            {
                TestInstance.PublicProperty = "test";
            }
            _stopWatch.Stop();
            Console.WriteLine($"Public set property: {_stopWatch.ElapsedMilliseconds}");

            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            for (var i = 0; i < Delay; i++)
            {
                ps1(TestInstance, "test");
            }
            _stopWatch.Stop();
            Console.WriteLine($"Public set property retriever: {_stopWatch.ElapsedMilliseconds}");

            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            for (var i = 0; i < Delay; i++)
            {
                ps2(TestInstance, "test");
            }
            _stopWatch.Stop();
            Console.WriteLine($"Internal set property retriever: {_stopWatch.ElapsedMilliseconds}");

            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            for (var i = 0; i < Delay; i++)
            {
                ps3(TestInstance, "test");
            }
            _stopWatch.Stop();
            Console.WriteLine($"Protected set property retriever: {_stopWatch.ElapsedMilliseconds}");

            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            for (var i = 0; i < Delay; i++)
            {
                ps4(TestInstance, "test");
            }
            _stopWatch.Stop();
            Console.WriteLine($"Private set property retriever: {_stopWatch.ElapsedMilliseconds}");
        }
    }
}