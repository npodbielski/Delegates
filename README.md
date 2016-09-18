# Delegates
Easy way to create delegates for obtaining/using all member types (fields, properties, indexers, methods, constructors and events) of all types (public and non public) and all visibilities (public, internal, protected and private)

Code is written as an example for articles:

http://internetexception.com/post/2016/08/05/Faster-then-Reflection-Delegates.aspx

http://internetexception.com/post/2016/08/16/Faster-than-Reflection-Delegates-Part-2.aspx

http://internetexception.com/post/2016/09/02/Faster-than-Reflection-Delegates-Part-3.aspx

Articles are also available on CodeProject:

http://www.codeproject.com/Articles/1118828/Faster-than-Reflection-Delegates-Part - part 1

http://www.codeproject.com/Articles/1124966/Faster-than-Reflection-Delegates-Part - part 2

http://www.codeproject.com/Articles/1124863/Faster-than-Reflection-Delegates-Part - part 3

##Examples

Below few examples of use of DelegateFactory

###Constructors

`var cd = DelegateFactory.DefaultContructor<TestClass>();`

`var c1 = DelegateFactory.Contructor<Func<TestClass>>();`

`var c2 = DelegateFactory.Contructor<Func<int, TestClass>>();`

`var c3 = Type.Contructor(typeof(int));`

###Static Properties

####Getters

`var spg1 = DelegateFactory.StaticPropertyGet<TestClass, string>("StaticPublicProperty");`

`var spg2 = Type.StaticPropertyGet<string>("StaticPublicProperty");`

`var spg3 = Type.StaticPropertyGet("StaticPublicProperty");`

####Setters

`var sps1 = DelegateFactory.StaticPropertySet<TestClass, string>("StaticPublicProperty");`

`var sps2 = Type.StaticPropertySet<string>("StaticPublicProperty");`

`var sps3 = Type.StaticPropertySet("StaticPublicProperty");`

###Instance Properties

####Getters

`var pg1 = DelegateFactory.PropertyGet<TestClass, string>("PublicProperty");`

`var pg2 = Type.PropertyGet<string>("PublicProperty");`

`var pg3 = Type.PropertyGet("PublicProperty");`

####Setters

`var ps1 = DelegateFactory.PropertySet<TestClass, string>("PublicProperty");`

`var ps2 = Type.PropertySet<string>("PublicProperty");`

`var ps3 = Type.PropertySet("PublicProperty");`

###Indexers

####Getters

`var ig1 = DelegateFactory.IndexerGet<TestClass, int, int>();`

`var ig2 = Type.IndexerGet<int, int>();`

`var ig3 = Type.IndexerGet(typeof(int), typeof(int));`

####Setters

`var is1 = DelegateFactory.IndexerSet<TestClass, int, int>();`

`var is2 = Type.IndexerSet(typeof(int), typeof(int));`

`var is3 = Type.IndexerSet<int, int>();`

###Static Fields

####Get value

`var sfg1 = DelegateFactory.StaticFieldGet<TestClass, string>("StaticPublicField");`

`var sfg2 = Type.StaticFieldGet<string>("StaticPublicField");`

`var sfg3 = Type.StaticFieldGet("StaticPublicField");`

####Set value

`var sfs1 = DelegateFactory.StaticFieldSet<TestClass, string>("StaticPublicField");`

`var sfs2 = Type.StaticFieldSet<string>("StaticPublicField");`

`var sfs3 = Type.StaticFieldSet("StaticPublicField");`

###Instance Fields

####Get value

`var fg1 = DelegateFactory.FieldGet<TestClass, string>("PublicField");`

`var fg2 = Type.FieldGet<string>("PublicField");`

`var fg3 = Type.FieldGet("PublicField");`

####Set value

`var fs1 = DelegateFactory.FieldSet<TestClass, string>("PublicField");`

`var fs2 = Type.FieldSet<string>("PublicField");`

`var fs3 = Type.FieldSet("PublicField");`

###Static Methods

`var sm1 = DelegateFactory.StaticMethod<TestClass, Func<string, string>>("StaticPublicMethod");`

`var sm2 = Type.StaticMethod<Func<string, string>>("StaticPublicMethod");`

`var sm3 = Type.StaticMethod("StaticPublicMethod", typeof(string));`

`var sm4 = Type.StaticMethodVoid("StaticPublicMethodVoid", typeof(string));`

###Instance Methods

`var m1 = DelegateFactory.InstanceMethod<Func<TestClass, string, string>>("PublicMethod");`

`var m2 = DelegateFactory.InstanceMethod2<Func<TestClass, string, string>>("PublicMethod");`

`var m3 = Type.InstanceMethod("PublicMethod", typeof(string));`

`var m4 = Type.InstanceMethodVoid("PublicMethodVoid", typeof(string));`

###Generic Methods

####Static

`var sg1 = DelegateFactory.StaticMethod<TestClass, Func<TestClass, TestClass>, TestClass>("StaticGenericMethod");`

`var sg2 = Type.StaticMethod<Func<TestClass, TestClass>, TestClass>("StaticGenericMethod");`

`var sg3 = Type.StaticGenericMethod("StaticGenericMethod", new[] { Type }, new[] { Type });`

`var sg4 = Type.StaticGenericMethodVoid("StaticGenericMethodVoid", new[] { Type }, new[] { Type });`

####Instance

`var ig1 = DelegateFactory.InstanceMethod<Func<TestClass, TestClass, TestClass>, TestClass>("GenericMethod");`

`var ig2 = Type.InstanceMethod<Func<TestClass, TestClass, TestClass>, TestClass>("GenericMethod");`

`var ig3 = Type.InstanceMethod<Func<object, TestClass, TestClass>, TestClass>("GenericMethod");`

`var ig4 = Type.InstanceGenericMethod("GenericMethod", new[] { Type }, new[] { Type });`

`var ig5 = Type.InstanceGenericMethodVoid("GenericMethodVoid", new[] { Type }, new[] { Type });`

###Events 

####Add Accessors

`var ea1 = DelegateFactory.EventAdd<TestClass, TestClass.PublicEventArgs>("PublicEvent");`

`var ea2 = Type.EventAdd<TestClass.PublicEventArgs>("PublicEvent");`

`var ea3 = Type.EventAdd("PrivateEvent");`

####Remove Accessors

`var er1 = DelegateFactory.EventRemove<TestClass, TestClass.PublicEventArgs>("PublicEvent");`

`var er4 = Type.EventRemove<TestClass.PublicEventArgs>("PublicEvent");`

`var er5 = Type.EventRemove("PrivateEvent");`
