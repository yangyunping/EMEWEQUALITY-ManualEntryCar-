﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace EMEWEEntity
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="CQEMEWEQC")]
	public partial class DataClasses1DataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region 可扩展性方法定义
    partial void OnCreated();
    partial void InsertU9Start(U9Start instance);
    partial void UpdateU9Start(U9Start instance);
    partial void DeleteU9Start(U9Start instance);
    #endregion
		
		public DataClasses1DataContext() : 
				base(global::EMEWEEntity.Properties.Settings.Default.CQEMEWEQCConnectionString4, mappingSource)
		{
			OnCreated();
		}
		
		public DataClasses1DataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DataClasses1DataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DataClasses1DataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DataClasses1DataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<U9Start> U9Start
		{
			get
			{
				return this.GetTable<U9Start>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.U9Start")]
	public partial class U9Start : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _U9ID;
		
		private string _U9Name;
		
		private bool _U9Bool;
		
    #region 可扩展性方法定义
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnU9IDChanging(int value);
    partial void OnU9IDChanged();
    partial void OnU9NameChanging(string value);
    partial void OnU9NameChanged();
    partial void OnU9BoolChanging(bool value);
    partial void OnU9BoolChanged();
    #endregion
		
		public U9Start()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_U9ID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int U9ID
		{
			get
			{
				return this._U9ID;
			}
			set
			{
				if ((this._U9ID != value))
				{
					this.OnU9IDChanging(value);
					this.SendPropertyChanging();
					this._U9ID = value;
					this.SendPropertyChanged("U9ID");
					this.OnU9IDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_U9Name", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string U9Name
		{
			get
			{
				return this._U9Name;
			}
			set
			{
				if ((this._U9Name != value))
				{
					this.OnU9NameChanging(value);
					this.SendPropertyChanging();
					this._U9Name = value;
					this.SendPropertyChanged("U9Name");
					this.OnU9NameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_U9Bool", DbType="Bit NOT NULL")]
		public bool U9Bool
		{
			get
			{
				return this._U9Bool;
			}
			set
			{
				if ((this._U9Bool != value))
				{
					this.OnU9BoolChanging(value);
					this.SendPropertyChanging();
					this._U9Bool = value;
					this.SendPropertyChanged("U9Bool");
					this.OnU9BoolChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
#pragma warning restore 1591
