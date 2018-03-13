Develop branch | Master branch 

[![Develop Status](http://185.157.80.85:8080/buildStatus/icon?job=Delegates_Develop)](http://185.157.80.85:8080/buildStatus/icon?job=Delegates_Develop)    [![Master Status](http://185.157.80.85:8080/buildStatus/icon?job=Delegates)](http://185.157.80.85:8080/buildStatus/icon?job=Delegates) 

# Delegates
Easy way to create delegates for obtaining/using all member types (fields, properties, indexers, methods, constructors and events) of all types (static and intance, generic and not generic) and all visibilities (public, internal, protected and private). 

Code was originally written as an example for below articles now is available as nuget package.

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

####Default Constructors

Default constructors of types can be converted to delegates very easily. For example if we have class TestClass with only default constructor:

`public class TestClass
{
   public TestClass()
   {
   }
}`

delegate for default constructor can be obtained with DelegateFactory in following ways:

1. If you are have access to type in compile time:

 `var cd = DelegateFactory.DefaultConstructor<TestClass>();`

 Created delegate in such case is `Func<TestClass>`
 The same delegate can be created with Constructor method:

 `var cd = DelegateFactory.Constructor<Func<TestClass>>();`

2. If you do not have access to type in compile time (private type, dynamic or otherwise inaccessible)

 `var cd = TestClassType.DefaultConstructor();`

 Created delegate in such case is `Func<object>`
 The same delegate can be created with Constructor method:

 `var cd = TestClassType.Constructor<Func<object>>();`

 Return value is of object type, so we cannot access its members directly, without casting or if it is not possible, members can be accessed via delegates.

3. If for some reason you want to create delegate to different constructor depending on some arbitrary, runtime conditions or you do not have access to parameter types in compile time, Constructor(params Type[]) method overload can be used:

 `var cd = TestClassType.Constructor();`

 Created delegate in such case is `Func<object[]. object>`
 You can call it in following way:

 `var instance = c(null);`
 `var instance = c(new object[0]);`

 Return value is of object type.

####Constructors

If TestClass have constructor with single parameter:

`internal TestClass(int p)
{
}`

delegate can be created in following ways:

1. If you are have access to type in compile time:

 `var c = DelegateFactory.Constructor<Func<int, TestClass>>();`

 Created delegate can be called as any other method that accepts single parameter of Int type:

 `var instance = c(0);`

2. If you do not have access to type in compile time:

 `var c = TestClassType.Constructor<Func<int, object>>();`

 Created delegate can be called as any other method that accepts single parameter of Int type:

 `var instance = c(0);`

 Return value is type of object, so we cannot access its members directly, without casting or if it is not possible, members can be accessed via delegates.

3. If for some reason you want to create delegate to different constructor depending on some arbitrary, runtime conditions or you do not have access to parameter types in compile time, Constructor(params Type[]) method overload can be used:

 `var c = TestClassType.Constructor(typeof(int));`

 Created delegate in such case is `Func<object[]. object>`
 You can call it in following way:

 `var instance = c(new object[] { 0 });`
 
 Created instance is type of object.

Delegates can be created for any constructor (visibility do not matter) with any combination of parameters. There is no limitation for number of parameters of constructor. All of above methods works for both classes and structures.

If you have access to all neccessary types and can create compatible delegate type, `Constructor<TDelegate>` method can be used. TDelegate type must follow these rules:
- parameters of delegate *must be* exactly of the same types and order as constructor
- return type of delegate *must be* type with constructor

I.e. if you constructor have 3 parameters:

`private TestClass(string p, int i, bool b)`

TDelegate *must be* `Func<string,int,bool,TestClass>`. It is because collection of constructor parameters are taked from TDelegate definition and constructor is searched in return type of defined TDelegate.

`var c = DelegateFactory.Constructor<Func<string, int, bool, TestClass>>();`

There is no limitation for type of delegate that can be accepted. For example, it can be `Func<>`, but do not have to. You can create your own delegate type, but it is must follow above restrictions.

`public delegate TestClass CustomCtrSingleParam(int i);
var cd = DelegateFactory.Constructor<CustomCtrSingleParam>();
var instance = cd(0);`


If you do not have access to type with constructor in compile time and still have access to constructor parameters types, `Constructor<TDelegate>(this Type)` extension method can be used. In this case return type of delegate can be any type that can be casted from source type (i.e. interface of TestClass, base type or object).

`var c = TestClassType.Constructor<Func<string, string, DateTime, object>>();`

Restriction for parameters types and order still applies. 


However if you do not have access to parameters types `Constructor(this Type, params Type[])` extension method ca be used. Array of parameters types must be *exactly the same* order and length as type of parameters of constructor. Overload do not require any delegate type. Instead have fixed delegate to `Func<object[], object>`. First parameter of created delegate is array of all constructor parameters values. It can be larger than required collection. Returned object is created instance. There is no limitation of number of parameters of constructor.

`var c = TestClassType.Constructor(typeof(bool));`
`var c = TestClassType.Constructor(typeof(string), typeof(bool), typeof(IDisposable));`


###Static Properties

Delegate factory can create delegate for any static property get or set accessor from either structure or class type. Visibility of accessor do not matter. Delegate for accessor can be created despite of existence of second accessor.

####Getters

If you have static property like below:

`public static string StaticPublicProperty { get; set; } = "StaticPublicProperty";`

delegate for get accessor can be created in following ways:

1. If you have access to type with defined static property and to property type in compile time:

 `var spg = DelegateFactory.StaticPropertyGet<TestClass, string>("StaticPublicProperty");`

 Of course first type parameter of `StaticPropertyGet` method have to be type with defined property with name as string parameter and second type parameter have to be type of property.
 Created delegate have no parameters and can executed to access property value as any other parameterless method.

 `var value = spg();`

2. If you do not have access to type with defined static property and have access to type of property in compile time:

 `var spg = TestClassType.StaticPropertyGet<string>("StaticPublicProperty");`
 
 Type parameter of `StaticPropertyGet` method have to be type of property and passed string have to be name of property.
 Created delegate have no parameters and can executed to access property value as any other parameterless method.
 
 `var value = spg();`

3. If you do not have access to either source type with property and property type:

 `var spg = TestClassType.StaticPropertyGet("StaticPublicProperty");`
 
 Created delegate returns object.
 
 `var objectValue = spg();`
 
Delegates for get accesor can be created from get only properties too.

`public static string StaticOnlyGetProperty { get; } = "StaticOnlyGetProperty";`

####Setters

If you have static property like below:

`public static string StaticPublicProperty { get; set; } = "StaticPublicProperty";`

delegate for get accessor can be created in following ways:

1. If you have access to type with defined static property and to property type in compile time:

 `var sps = DelegateFactory.StaticPropertySet<TestClass, string>("StaticPublicProperty");`

 Of course first type parameter of `StaticPropertySet` method have to be type with defined property with name as string parameter and second type parameter have to be type of property.
 Created delegate have single parameter (with new property value) and can executed as any other void method with single parameter.

 `sps(newValue);`

2. If you do not have access to type with defined static property and have access to type of property in compile time:

 `var spg = TestClassType.StaticPropertySet<string>("StaticPublicProperty");`
 
 Type parameter of `StaticPropertyGet` method have to be type of property and passed string have to be name of property.
 Created delegate have single parameter (with new property value) and can executed as any other void method with single parameter.
 
 `sps(newValue);`

3. If you do not have access to either source type with property and property type:

 `var sps = TestClassType.StaticPropertySet("StaticPublicProperty");`
 
 Created delegate takes single object parameter and does not return value.
 
 `sps(newValue);`
 

###Instance Properties

Delegate factory can create delegate for any instance property get or set accessor from either structure or class type. Visibility of accessor do not matter. Delegate for accessor can be created despite of existence of second accessor.

####Getters

If you have instance property like below:

`public string PublicProperty { get; set; } = "PublicProperty";`

delegate for get accessor can be created in following ways:

1. If you have access to type with defined instance property and to property type in compile time:

 `var pg = DelegateFactory.PropertyGet<TestClass, string>("PublicProperty");`
 
 Of course first type parameter of `PropertyGet` method have to be type with defined property with name as string parameter and second type parameter have to be type of property.
 Created delegate have one parameter (source instance with property value) and can be executed to access property value as any other method with single parameter. Return type is the same as property type.
 
 `var value = pg(instance);`

2. If you do not have access to type with defined instance property and have access to type of property in compile time:

 `var pg = TestClassType.PropertyGet<string>("PublicProperty");`
 
 Type parameter of `PropertyGet` method have to be type of property and passed string have to be name of property.
 Created delegate have one parameter (source instance as object with property value) and can be executed to access property value as any other method with single parameter. Return type is the same as property type.

 `var value = pg(instanceAsObject);`
 
3. If you do not have access to either source type with property and property type:

 `var pg = TestClassType.PropertyGet("PublicProperty");`
 
 String passed to `PropertyGet` method have to be name of property. 
 Created delegate returns object and require an object as parameter with source instance.

 `var objectValue = pg(instanceAsObject);`
 
Delegates for get accesor can be created from get only properties too.

`public string OnlyGetProperty => "OnlyGetProperty";`

####Setters

If you have instance property like below:

`public string PublicProperty { get; set; }`

delegate for set accessor can be created in following ways:

1. If you have access to type with defined instance property and to property type in compile time:

 `var ps = DelegateFactory.PropertySet<TestClass, string>("PublicProperty");`

 Of course first type parameter of `PropertySet` method have to be type with defined property with name as string parameter and second type parameter have to be type of property.
 Created delegate have two parameters (instance with property and new property value) and can executed as any other void method with two parameters.

 `ps(instance, newValue);`
 
2. If you do not have access to type with defined intance property and have access to type of property in compile time:

 `var ps = TestClassType.PropertySet<string>("PublicProperty");`
 
 Type parameter of `PropertyGet` method have to be type of property and passed string have to be name of property.
 Created delegate have two parameters (instance as object with property and new property value) and can executed as any other void method with two parameters.
 
 `ps(instanceAsObject, newValue);`
 
3. If you do not have access to either source type with property and property type:

 `var ps = TestClassType.PropertySet("PublicProperty");`
 
 Created delegate takes two object parameters and does not return value.
 
 `ps(instanceAsObject, newValueAsObject);`


###Indexers

Delegate factory can create delegate for any indexer get or set accessor from either structure or class type. Visibility of accessor or number of index parameters do not matter. Delegate for accessor can be created despite of existence of second accessor. Indexer name can be default or custom one. All methods that creates indexer delegates do not need string parameter with indexer name all indexers have to have the same name.

####Getters

If you have indexer defined like this:

`[IndexerName("TheItem")]
internal string this[string s] => s;`

delegate for get accessor can be created in following ways:

1. If you have access to type with defined indexer, to indexer all indexes types and return type in compile time:

 `var ig = DelegateFactory.IndexerGet<TestClass, string, string>();`

 First type parameter has to be type with defined indexer, next type paraters are (in above case single one) indexes types and last one is return type of indexer and created delegate. Created delegate have as many parameters as indexer have indexes, plus one (first) for instance and return value of indexer.
 
 `var value = ig(instance, index)`

2. If you do not have access to type with defined indexer and have access to indexer all indexes types and return type in compile time:

 `var ig = TestClassType.IndexerGet<string, string>();`
 
 All type parameters beside the last one are types of indexer indexes and last one is indexer return type. Created delegate have as many parameters as indexer have indexes, plus one (first) for instance as object and return value of indexer.

 `var value = ig(instanceAsObject, index)`
 
3. If you do not have access to either source type with indexer, one or all of indexer indexes types and/or indexer return type:

 `var ig = TestClassType.IndexerGetNew(typeof(string));`
 
 This overload require single type as parameter because set of indexes types is enough to identify particular indexer (it is impossible to create two indexers with the same indexes, but different return type). Created delegate takes two parameters: source instance with indexer as object and array of objects with indexes values (array ca be bigger than number of indexes). Returned value by delegate is of object type.
 
 `var valueAsObject = ig(instanceAsObject, new object []{ indexAsObject })`
 
 

####Setters

If you have indexer defined like this:

internal double this[double s]
{
 get { //return value }
 set { //save value }
}

delegate for set accessor can be created in following ways:

1. If you have access to type with defined indexer, to indexer all indexes types and return type in compile time:

 `var @is = DelegateFactory.IndexerSet<TestClass, double, double>();`
 
 First type parameter is type with defined indexer. Second is type of index and last is type of indexer value to set at given index.
 Created delegate takes three parameters (source instance, index value and value to set) and does not return value.
 
 `@is(instance, index, newValue);`
 
 There is also an overload of above generic method that takes 2 and 3 indexes types for indexers that have accordingly 2 and 3 indexes. If you need more indexes than that you can use non-generic overload of `IndexerSet` (like in case 3).
 
2. If you do not have access to type with defined indexer and have access to indexer all indexes types and return type in compile time:

 `var @is = TestClassType.IndexerSet<double, double>();`

 All type parameters beside the last one are types of indexer indexes and last one is type of value to set.
 Created delegate have as many parameters as indexer have indexes, plus one (first) for instance as object, and plus one for new value of indexer at given index.

 `@is(instanceAsObject, index, newValue);`
 
 There is also an overload of above generic method that takes 2 and 3 indexes types for indexers that have accordingly 2 and 3 indexes. If you need more indexes than that you can use non-generic overload of `IndexerSet` (like in case 3).
 
3. If you do not have access to either source type with indexer, one or all of indexer indexes types and/or indexer return type:

 `var is = TestClassType.IndexerSetNew(typeof(double));`
 
 This overload require single type as parameter because set of indexes types is enough to identify particular indexer (it is impossible to create two indexers with the same indexes, but different return type). Created delegate takes two parameters: source instance with indexer as object and array of objects with indexes values (array ca be bigger than number of indexes). Returned value by delegate is of object type.
 
 `var valueAsObject = ig(instanceAsObject, new object []{ indexAsObject })`
 


###Static Fields

DelegateFactory can create delegates for retrieving or setting (unless field is readonly) any type static field of any visibility.

####Get value

For example if you have static field defined like below:

`public static string StaticPublicField = "StaticPublicField";`

you can create delegate in one of following ways:

1. If you have access to type with defined static field and to type of field in compile time:

 `var sfg = DelegateFactory.StaticFieldGet<TestClass, string>("StaticPublicField");`
 
 First generic parameter is type with field defined and second is type of field. String parameter is name of field.
 Created delegate do not take any parameters and can be used as any parameterless method. Return value of delegate is value of a field.
 
 `var value = sfg()`
 
2. If you do not have access to type with defined static field and have to type of field in compile time:

 `var sfg = TestStructType.StaticFieldGet<string>("StaticPublicField");`
 
 Generic parameter is type of field and string parameter is name of field. Created delegate takes no parameters and return value of static field.
 
 `var value = sfg()`
 
 
3. If you do not have access to type with defined static field and to type of field in compile time:

 `var sfg = TestClassType.StaticFieldGet("StaticPublicField");`
 
 String parameter is name of field. Created delegate takes no parameters and return value of static field as object
 
 `var valueAsObject = sfg()`


####Set value

For example if you have *non readonly* static field defined like below:

`public static string StaticPublicField = "StaticPublicField";`

you can create delegate in one of following ways:

1. If you have access to type with defined static field and to type of field in compile time:

 `var sfs = DelegateFactory.StaticFieldSet<TestClass, string>("StaticPublicField");`
 
 First generic parameter is type with field defined and second is type of field. String parameter is name of field.
 Created delegate do take one parameter (new value), do not return value and can be used as any method with one parameter.

 `sfs(newValue);`
 
2. If you do not have access to type with defined static field and have to type of field in compile time:

 `var sfs = Type.StaticFieldSet<string>("StaticPublicField");`
 
 Generic parameter is type of field and string parameter is name of field. Created delegate takes one parameter (value of static field) and do not return value.
 
 `sfs(newValue);`
 
3. If you do not have access to type with defined static field and to type of field in compile time:

 `var sfs = Type.StaticFieldSet("StaticPublicField");`
 
 String parameter is name of field. Created delegate takes one parameter (value of field as object) and do not return value.
 
 `sfs(valueAsObject)`
 
 
All above methods will not return delegate for fields marked with `readonly` keyword.


###Instance Fields

DelegateFactory can create delegates for retrieving or setting (unless field is readonly) any type instance field of any visibility.

####Get value

For example if you have instance field defined like below:

`public string PublicField;`

you can create get value delegate in one of following ways:

1. If you have access to type with defined instance field and to type of field in compile time:

 `var fg = DelegateFactory.FieldGet<TestClass, string>("PublicField");`
 
 First generic parameter is type with field defined and second is type of field. String parameter is name of field.
 Created delegate take one parameter (instance of type) and can be used as any method with one parameter. Return value of delegate is value of a field.
 
 `var value = fg(instance)`
 
2. If you do not have access to type with defined static field and have to type of field in compile time:

 `var sfg = TestStructType.StaticFieldGet<string>("StaticPublicField");`
 
 Generic parameter is type of field and string parameter is name of field. Created delegate takes no parameters and return value of static field.
 
 `var value = sfg()`
 
 
3. If you do not have access to type with defined static field and to type of field in compile time:

 `var sfg = TestClassType.StaticFieldGet("StaticPublicField");`
 
 String parameter is name of field. Created delegate takes no parameters and return value of static field as object
 
 `var valueAsObject = sfg()`


####Set value

For example if you have *non readonly* instance field defined like below:

`public string PublicField;`

you can create set value delegate in one of following ways:

1. If you have access to type with defined instance field and to type of field in compile time:

 `var fs = DelegateFactory.FieldSet<TestClass, string>("PublicField");`
 
 First generic parameter is type with field defined and second is type of field. String parameter is name of field.
 Created delegate take two parameters (isntance and, new field value), do not return value and can be used as any method with two parameters.

 `fs(instance, newValue);`
 
2. If you do not have access to type with defined instance field and have to type of field in compile time:

 `var sfs = TestClassType.FieldSet<string>("StaticPublicField");`
 
 Generic parameter is type of field and string parameter is name of field. Created delegate takes two parameters (instance as object and value of field) and do not return value.
 
 `fs(instanceAsObject, newValue);`
 
3. If you do not have access to type with defined static field and to type of field in compile time:

 `var fs = TestClassType.FieldSet("PublicField");`
 
 String parameter is name of field. Created delegate takes two parameter (instance as object and value of field as object) and do not return value.
 
 `fs(instanceAsObject, valueAsObject)`
 
 
All above methods will not return delegate for fields marked with `readonly` keyword.


###Static Methods

DelegateFactory can create delegates for any static method with any number od parameters and of any visibility.

*For now custom delegates with parameters of `out` or `ref` modifiers are not allowed!*

In example for static method like below:

`public static string StaticPublicMethod(string s)
{
  return s;
}`
        
you can create delegate in following ways:

1. If you have access to type with defined static method, to parameters types and to return type of method in compile time:

 `var sm = DelegateFactory.StaticMethod<TestClass, Func<string, string>>("StaticPublicMethod");`
 
 First generic parameter is type with method and second is type of delegate to create. String parameter is name of static method. 
 Created delegate take as many parameters as defined in delegate type passed as second type parameter, which have to be compatible with static method signature.

 `var result = sm(parameterValue);`
 
2. If you do not have access to type with defined static method, but have to parameters types and to return type of method in compile time:

 `var sm2 = TestClassType.StaticMethod<Func<string, string>>("StaticPublicMethod");`
 
 Generic parameter is type of delegate to create, which have to be compatible with static method signature. String parameter is name of static method. Created delegate take as many parameters as defined in delegate type passed as second type parameter.
 
 `var result = sm(parameterValue);`
 
3. If you do not have access to any or only part of types (type with defined static method, parameters types and to return type of method) in compile time:

 `var sm = TestClassType.StaticMethod("StaticPublic", typeof(string));`
 
 String parameter is name of static method. Second parameter is type of parameter. Created delegate takes two parameters (instance as object and array of objects with parameters values).
 
 `var result = sm(new object[] { parameterValueAsObject });`
 
 With above `StaticMethod` method overload you can create delegate only to static methods that have non-void signature. If you want delegate to void method use `StaticMethodVoid` method of `DelegatesFactory` class.
 
This way you can create delegate for any static methods with any number of parameters. If number of parameters exceeds number of parameters allowed in .NET classes of `Func<>` and `Action<>` you can create you own delegate with more parameters. Also custom types of delegates are allowed for smaller number of parameters. 

###Instance Methods

DelegateFactory can create delegates for any instance method with any number od parameters and of any visibility.

*For now custom delegates with parameters of `out` or `ref` modifiers are not allowed!*

In example for instance method like below:

`public string PublicMethod(string s)
{
  return s;
}`        
        
you can create delegate in following ways:

1. If you have access to type with defined instance method, to parameters types and to return type of method in compile time:

 `var m = DelegateFactory.InstanceMethod<Func<TestClass, string, string>>("PublicMethod");`
 
 Generic parameter is type of delegate to create. String parameter is name of instance method. 
 Created delegate take as many parameters as defined in delegate type as type parameter, which have to be compatible with instance method signature, but have to take instance parameter as its first parameter.

 `var result = m(instance, parameterValue);`
 
 If your instance method is void instead of non-void just use `Action<>` delegate instead of `Func<>`.
 
2. If you do not have access to type with defined instance method, but have to parameters types and to return type of method in compile time:

 `var m = TestClassType.InstanceMethod<Func<object, string>>("PublicMethod");`
 
 Generic parameter is type of delegate to create, which have to be compatible with instance method signature. String parameter is name of instance method. Created delegate take as many parameters as defined in delegate type as type parameter, which have to be compatible with instance method signature, but have to take instance parameters as its first parameter.
 
  `var result = m(instanceAsObject, parameterValue);`
  
 It is also possible to pass delegate with instance type instead of object, but if this is the case it is recomended to use previous method instead. Also you can pass delegate that have instance parameter type of some base class or interface implemented in type with defined method.
 
3. If you do not have access to any or only part of types (type with defined instance method, parameters types and to return type of method) in compile time:

 `var m = TestClassType.InstanceMethod("PublicMethod", typeof(string));`
 
 String parameter is name of instance method. Second parameter is type of parameter. Created delegate takes two parameters (instance as object and array of objects with parameters values).
 
 `var result = m(instanceAsObject, new object[] { parameterValueAsObject });`
 
 With above `InstanceMethod` method overload you can create delegate only to instance methods that have non-void signature. If you want delegate to void method use `InstanceMethodVoid` method of `DelegatesFactory` class.
 
This way you can create delegate for any instance methods with any number of parameters. If number of parameters exceeds number of parameters allowed in .NET classes of `Func<>` and `Action<>` you can create you own delegate with more parameters. Also custom types of delegates are allowed for smaller number of parameters. 


###Generic Methods

DelegateFactory can create delegates for any instance or static generic method with any number of type parameters and of any visibility. Generic type parameters constraints are also supported.

####Static

If you have static generic method defined as below:

`public static T StaticGenericMethod<T>(T param)
{
  return param;
}`

you can create delegate in one of following ways:

1. If you have access to type with defined static method, to parameters types, to generic parameters types and to return type of method in compile time:

 `var sg = DelegateFactory.StaticMethod<TestClass, Func<TestClass, TestClass>, TestClass>("StaticGenericMethod");`
 
 First generic parameter is type with defined method. Second is type of delegate to create. Last type parameter is type parameter of static generic method. String parameter is name of instance method. 
 Created delegate take as many parameters as defined in delegate type as type parameter, which have to be compatible with static generic method signature.

 `var result = m(parameterValue);`
 
2. If you do not have access to type with defined static generic method, but have to parameters types, generic pamaters types and to return type of method in compile time:

 `var sg = TestClassType.StaticMethod<Func<TestClass, TestClass>, TestClass>("StaticGenericMethod");`
 
 First generic parameter is type of delegate to create, which have to be compatible with static generic method signature. Second type parameter is type parameter of static generic method. String parameter is name of instance method. Created delegate take as many parameters as defined in delegate type as type parameter, which have to be compatible with static generic method signature.
 
 `var result = m(parameterValue);`
  
3. If you do not have access to any or only part of types (type with defined static generic method, parameters types, generic parameters types and to return type of method) in compile time:

 `var sg = TestClassType.StaticGenericMethod("StaticGenericMethod", new[] { TestClassType }, new[] { TestClassType });`
 
 String parameter is name of instance method. Second parameter is collection of parameters types of instance generic method. Last parameter is collection of type parameters types. Created delegate takes one parameter with array of objects as parameters for static generic method. Returned value is of object type.
 
 `var resultAsObject = sg(new object[] { parameterValueAsObject });`
 
 With above `StaticGenericMethod` method overload you can create delegate only to static methods that have non-void signature. If you want delegate to void method use `StaticGenericMethodVoid` method of `DelegatesFactory` class.
 
 `var sg = TestClassType.StaticGenericMethodVoid("StaticGenericMethodVoid", new[] { TestClassType }, new[] { TestClassType });`

####Instance

If you have instance generic method defined as below:

`public T GenericMethod<T>(T s)
{
  return s;
}`

you can create delegate in one of following ways:

1. If you have access to type with defined instance generic method, to parameters types, to generic parameters types and to return type of method in compile time:

 `var ig = DelegateFactory.InstanceMethod<Func<TestClass, TestClass, TestClass>, TestClass>("GenericMethod");`
 
 Second is type of delegate to create. Last type parameter is type parameter of static generic method. String parameter is name of instance method. 
 Created delegate take as many parameters as defined in delegate type as type parameter, which have to be compatible with static generic method signature, but have to take instance parameter as its first parameter.

 `var result = m(instance, parameterValue);`
 
2. If you do not have access to type with defined instance generic method, but have to parameters types, generic pamaters types and to return type of method in compile time:

 `var ig = TestClassType.InstanceMethod<Func<object, TestClass, TestClass>, TestClass>("GenericMethod");`
 
 First generic parameter is type of delegate to create, which have to be compatible with instance generic method signature. Second type parameter is type parameter of instance generic method. String parameter is name of instance method. Created delegate take as many parameters as defined in delegate type as type parameter, which have to be compatible with static generic method signature, but have to take instance parameter as object as its first parameter.
 
 `var result = m(instanceAsObject, parameterValue);`
 
 There is also possibility to create the same delegate as in first example with the same `InstanceMethod` overload:
 
 `var ig3 = TestClassType.InstanceMethod<Func<TestClass, TestClass, TestClass>, TestClass>("GenericMethod");`
 
 but it is recomended to use firts overload in this case instead.
  
3. If you do not have access to any or only part of types (type with defined instance generic method, parameters types, generic parameters types and to return type of method) in compile time:

 `var ig = TestClassType.InstanceGenericMethod("GenericMethod", new[] { TestClassType }, new[] { TestClassType });`
 
 String parameter is name of instance method. Second parameter is collection of parameters types of instance generic method. Last parameter is collection of type parameters types. Created delegate takes two parameters: instance as object and array of objects as parameters for instance generic method. Returned value is of object type.
 
 `var resultAsObject = ig(new object[] { parameterValueAsObject });`
 
 With above `InstanceGenericMethod` method overload you can create delegate only to static methods that have non-void signature. If you want delegate to void method use `InstanceGenericMethodVoid` method of `DelegatesFactory` class.
 
 `var ig = TestClassType.InstanceGenericMethodVoid("GenericMethodVoid", new[] { TestClassType }, new[] { TestClassType });`
 


###Events 

DelegateFactory can create delegates for any instance event and of any visibility. Event can have auto generated or custom accessors.
Static events are not supported.

####Add Accessors

If you have event defined like below:

`public event EventHandler<PublicEventArgs> PublicEvent;`

you can create delegate for add accessor in following ways:

1. If you have access to type with defined event and to event arguments type in compile time:

 `var ea = DelegateFactory.EventAdd<TestClass, TestClass.PublicEventArgs>("PublicEvent");`
 
 First generic type parameter is type with event. Second one is event arguments type. String parameter is name of event.
 Created delegate takes two parameters: instance and event handler to set on event. Handler have type of `EventHandler<TestClass.PublicEventArgs>`.
 
 `ea(instance, handler);`
 
2. If you do not have access to type with defined event and have to event arguments type in compile time:

 `var ea = Type.EventAdd<TestClass.PublicEventArgs>("PublicEvent");`

 Generic type parameter is event arguments type. String parameter is name of event. Created delegate takes two parameters: instance as object and event handler to set on event. Handler have type of `EventHandler<TestClass.PublicEventArgs>`.
 
 `ea(instanceAsObject, handler);`
 
3. If you do not have access to type with defined event and to event arguments type in compile time:

 `var ea = Type.EventAdd("PublicEvent");`
 
 String parameter is name of event. Created delegate takes two parameters: instance as object and event handler to set on event. Handler have type of `Action<object, object>`.
 
 `ea(instanceAsObject, actionHandler);`
 
 

####Remove Accessors

If you have event defined like below:

`public event EventHandler<PublicEventArgs> PublicEvent;`

you can create delegate for remove accessor in following ways:

1. If you have access to type with defined event and to event arguments type in compile time:

 `var er = DelegateFactory.EventRemove<TestClass, TestClass.PublicEventArgs>("PublicEvent");`
 
 First generic type parameter is type with event. Second one is event arguments type. String parameter is name of event.
 Created delegate takes two parameters: instance and event handler to remove from event. Handler have type of `EventHandler<TestClass.PublicEventArgs>`.
 
 `er(instance, handler);`
 
2. If you do not have access to type with defined event and have to event arguments type in compile time:

 `var er = Type.EventRemove<TestClass.PublicEventArgs>("PublicEvent");`

 Generic type parameter is event arguments type. String parameter is name of event. Created delegate takes two parameters: instance as object and event handler to remove from event. Handler have type of `EventHandler<TestClass.PublicEventArgs>`.
 
 `er(instanceAsObject, handler);`
 
3. If you do not have access to type with defined event and to event arguments type in compile time:

 `var er = Type.EventRemove("PublicEvent");`
 
 String parameter is name of event. Created delegate takes two parameters: instance as object and event handler to remove from event. Handler have type of `Action<object, object>`.
 
 `er(instanceAsObject, actionHandler);`
