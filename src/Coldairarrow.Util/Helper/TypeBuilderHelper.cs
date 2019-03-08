using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace Coldairarrow.Util
{
    public interface ITest
    {
        string Hello(string str);
    }

    /// <summary>
    /// 运行时创建类型
    /// </summary>
    public static class TypeBuilderHelper
    {
        /// <summary>
        /// 创建类型
        /// </summary>
        /// <param name="typeFullName">类型完全名,包括命名空间</param>
        /// <param name="assemblyName">类型程序集名</param>
        /// <param name="properties">类型属性配置</param>
        /// <returns></returns>
        public static Type BuildType(string typeFullName, string assemblyName, List<PropertyConfig> properties)
        {
            TypeBuilder tb = GetTypeBuilder(typeFullName,assemblyName);
            //tb.AddInterfaceImplementation(typeof(ITest));
            ConstructorBuilder constructor = tb.DefineDefaultConstructor(MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.RTSpecialName);
            properties.ForEach(aProperty =>
            {
                AddProperty(tb, aProperty.PropertyName, aProperty.PropertyType, aProperty.CustomAttributes);
            });

            return tb.CreateTypeInfo();
        }

        private static TypeBuilder GetTypeBuilder(string typeFullName, string assemblyName)
        {
            var an = new AssemblyName(assemblyName);
            AssemblyBuilder assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(an, AssemblyBuilderAccess.Run);
            ModuleBuilder moduleBuilder = assemblyBuilder.DefineDynamicModule("MainModule");
            TypeBuilder tb = moduleBuilder.DefineType(typeFullName,
                    TypeAttributes.Public |
                    TypeAttributes.Class |
                    TypeAttributes.AutoClass |
                    TypeAttributes.AnsiClass |
                    TypeAttributes.BeforeFieldInit |
                    TypeAttributes.AutoLayout,
                    null);
            return tb;
        }
        private static void AddProperty(TypeBuilder tb, string propertyName, Type propertyType, List<Type> attributes)
        {
            FieldBuilder fieldBuilder = tb.DefineField("_" + propertyName, propertyType, FieldAttributes.Private);

            PropertyBuilder propertyBuilder = tb.DefineProperty(propertyName, PropertyAttributes.HasDefault, propertyType, null);
            MethodBuilder getPropMthdBldr = tb.DefineMethod("get_" + propertyName, MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.HideBySig, propertyType, Type.EmptyTypes);
            ILGenerator getIl = getPropMthdBldr.GetILGenerator();

            getIl.Emit(OpCodes.Ldarg_0);
            getIl.Emit(OpCodes.Ldfld, fieldBuilder);
            getIl.Emit(OpCodes.Ret);

            MethodBuilder setPropMthdBldr =
                tb.DefineMethod("set_" + propertyName,
                  MethodAttributes.Public |
                  MethodAttributes.SpecialName |
                  MethodAttributes.HideBySig,
                  null, new[] { propertyType });

            ILGenerator setIl = setPropMthdBldr.GetILGenerator();
            Label modifyProperty = setIl.DefineLabel();
            Label exitSet = setIl.DefineLabel();

            setIl.MarkLabel(modifyProperty);
            setIl.Emit(OpCodes.Ldarg_0);
            setIl.Emit(OpCodes.Ldarg_1);
            setIl.Emit(OpCodes.Stfld, fieldBuilder);

            setIl.Emit(OpCodes.Nop);
            setIl.MarkLabel(exitSet);
            setIl.Emit(OpCodes.Ret);

            propertyBuilder.SetGetMethod(getPropMthdBldr);
            propertyBuilder.SetSetMethod(setPropMthdBldr);

            //设置特性
            attributes?.ForEach(aAttribute =>
            {
                var attribute = aAttribute.GetConstructor(new Type[] { });
                var attributeBuilder = new CustomAttributeBuilder(attribute, new object[] { });
                propertyBuilder.SetCustomAttribute(attributeBuilder);
            });
        }
    }

    /// <summary>
    /// 类型属性配置
    /// </summary>
    public struct PropertyConfig
    {
        /// <summary>
        /// 属性名
        /// </summary>
        public string PropertyName { get; set; }

        /// <summary>
        /// 属性类型
        /// </summary>
        public Type PropertyType { get; set; }

        /// <summary>
        /// 属性包含的自定义特性
        /// </summary>
        public List<Type> CustomAttributes { get; set; }
    }
}