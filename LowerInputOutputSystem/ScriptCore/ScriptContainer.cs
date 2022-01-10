using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.ClearScript;
using Microsoft.ClearScript.V8;

namespace LowerInputOutputSystem
{
    public class ScriptContainer
    {
        // 脚本容器名字0
        public String name;
        
        // 脚本容器版本
        public float version;
        
        // V8Script 引擎
        private V8ScriptEngine engine;
        
        // 全局变量存储容器
        private List<GlobalVariable> _golbalVaribleList;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="version">版本</param>
        public ScriptContainer(string name, float version)
        {
            this.name = name;
            this.version = version;
            
            engine = new V8ScriptEngine(name);
            engine.DocumentSettings.AccessFlags = DocumentAccessFlags.EnableFileLoading;
            engine.DefaultAccess = ScriptAccess.Full;
            engine.AddHostObject("self", this);
            engine.AddHostObject("engine", engine);
                
            _golbalVaribleList = new List<GlobalVariable>();
        }

        /// <summary>
        /// 添加类
        /// </summary>
        /// <param name="engine">引擎</param>
        /// <param name="list">
        ///     待添加的类，参数使用 String ，类名以换行符隔开
        /// </param>
        public void AddTypes(V8ScriptEngine engine, string list)
        {
            foreach (string item in list.Split('\n'))
            {
                string[] split = item.Split('.');
                string name = split[split.Length - 1];
                engine.AddHostType(name, Type.GetType(item));
            }
        }
        
        /// <summary>
        /// 添加类
        /// </summary>
        /// <param name="engine">引擎</param>
        /// <param name="name">类名</param>
        public void AddType(V8ScriptEngine engine, string name)
        {
            string[] split = name.Split('.');
            string namef = split[split.Length - 1];
            engine.AddHostType(namef, Type.GetType(name));
        }

        /// <summary>
        /// 运行一串js代码
        /// </summary>
        /// <param name="code">代码</param>
        public void Execute(string code)
        {
            try {
                engine.Execute(code);
            }
            catch (ScriptEngineException exception)
            {
                Console.WriteLine("Err : engine {0}", exception.EngineName);
                Console.WriteLine(exception.ErrorDetails);
            }
        }

        /// <summary>
        /// 代理全局变量
        /// </summary>
        /// <param name="name"></param>
        /// <param name="variable"></param>
        public void AddGlobalVariable(string name, Object variable)
        {
            if (IndexGlobalVariable(name) == -1)
            {
                _golbalVaribleList.Add(
                    new GlobalVariable(name, variable)
                );
            }
            else
            {
                SetGlobalVariable(name, variable);
            }
        }
        
        /// <summary>
        /// 设置全局变量
        /// </summary>
        /// <param name="name"></param>
        /// <param name="variable"></param>
        public void SetGlobalVariable(string name, Object variable)
        {
            _golbalVaribleList[IndexGlobalVariable(name)].variable = variable;
        }

        /// <summary>
        /// 获得全局变量
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Object GetGlobalVariable(string name)
        {
            return _golbalVaribleList[IndexGlobalVariable(name)].variable;
        }

        /// <summary>
        /// 定位全局变量
        /// 当返回值为-1时说明变量并不存在
        /// </summary>
        /// <param name="name"></param>
        public int IndexGlobalVariable(string name)
        {
            int index = 0;
            foreach (GlobalVariable globalVariable in _golbalVaribleList)
            {
                if (globalVariable.name.Equals(name.Trim()))
                {
                    return index;
                }
                index ++;
            }
            return -1;
        }

        /// <summary>
        /// 全局变量类
        /// </summary>
        public class GlobalVariable
        {
            public Object variable;
            public string name;

            public GlobalVariable(string name, Object variable)
            {
                this.variable = variable;
                this.name = name.Trim();
            }
        }
    }
}