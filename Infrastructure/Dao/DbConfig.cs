/************************************************************************************
 * Copyright (c) 2016 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：Infrastructure.Dao
 * 文件名：DbConfig
 * 版本号：v1.0.0.0
 * 唯一标识：2bc4d0ef-9a50-4d80-ab19-89d540b68818
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2016/12/18 23:57:52
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2016/12/18 23:57:52
 * 修改人：简玄冰
 * 版本号：v1.0.0.0
 * 描述：
 *
/************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JCE.DapperContext;
using JCE.DapperContext.Entities;

namespace Infrastructure.Dao
{
    /// <summary>
    /// 数据库访问配置
    /// </summary>
    public class DbConfig
    {
        /// <summary>
        /// 默认过滤器
        /// </summary>
        public static Dictionary<string,Func<KeyValue<object>>> DefaultFilter=new Dictionary<string, Func<KeyValue<object>>>()
        {
            //单表查询
            {"FalseDelete",()=> {return new KeyValue<object>() {Key = " (isdeleted=0 or isdeleted is null) "};} },
            //多表查询
            {"FalseDeleteJoin",()=> {return new KeyValue<object>() {Key = " (m.isdeleted=0 or m.isdeleted is null) "};} }
        };
        /// <summary>
        /// 获取数据库访问实例
        /// </summary>
        /// <returns></returns>
        public static DapperDbContext GetDbInstance()
        {
            try
            {
                var result=new DapperDbContext("default",true);
                result.SetFilterItems(DefaultFilter);//给查询添加默认过滤器（所有查询加上 isdeleted=0 or null）
                result.AddDisableUpdateColumns("CreateTime","Creator");//添加禁止更新列
                result.CurrentFilterKey = "FalseDelete";
                result.IsEnableLogEvent = true;
                result.LogEventStarting = (sql, pars) => { };//打断点查看生成Sql语句
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception($"连接数据库出错，请检查您的连接字符串和网络。ex:{ex.Message}");
            }
        }
    }
}
