// Copyright 2013 ESRI
// 
// All rights reserved under the copyright laws of the United States
// and applicable international laws, treaties, and conventions.
// 
// You may freely redistribute and use this sample code, with or
// without modification, provided you include the original copyright
// notice and use restrictions.
// 
// See the use restrictions http://help.arcgis.com/en/sdk/10.0/usageRestrictions.htm.
// 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using client = ESRI.ArcGIS.Client;
using ESRI.ArcGIS.OperationsDashboard;
using System.Diagnostics;
using System.Windows.Threading;

namespace CountWidgetCS
{
  /// <summary>
  /// A Widget is a dockable extension to the ArcGIS Operations Dashboard application that implements IWidget. By returning true from CanConfigure, 
  /// this widget provides the ability for the user to configure the widget properties showing a settings Window in the Configure method.
  /// By implementing IDataSourceConsumer, tthis Widget indicates it requires a DataSource to function and will be notified when the 
  /// data source is updated or removed.
  /// By implementing INotifyPropertyChanged, any changes made to the widget Caption by the user during configuration is reflected immediately
  /// in the title bar of the widget within the application.
  /// </summary>
  [Export("ESRI.ArcGIS.OperationsDashboard.Widget")]
  [ExportMetadata("DisplayName", "CountWidget SDK Sample C#")]
  [ExportMetadata("Description", "WPF Runtime SDK sample widget that counts features containing a specific attribute value.")]
  [ExportMetadata("ImagePath", "/CountWidgetCS;component/Images/Calculator.png")]
  [ExportMetadata("DataSourceRequired", true)]
  [DataContract]
  public partial class CountWidget : UserControl, IWidget, IDataSourceConsumer
  {
    /// <summary>
    /// A unique identifier of a data source in the configuration. This property is set during widget configuration.
    /// </summary>
    [DataMember(Name = "dataSourceId")]
    public string DataSourceId { get; set; }

    /// <summary>
    /// The name of a field within the selected data source. This property is set during widget configuration.
    /// </summary>
    [DataMember(Name = "field")]
    public string FieldName { get; set; }

    private client.Field Field { get; set; }

    [DataMember(Name = "value")]
    public string Value { get; set; }

    private string QueryValue { get; set; }

    private static SmartPen.Pen Pen;

    public CountWidget()
    {
      InitializeComponent();

      Value = "0";
      Caption = "New Count Widget";
      Pen = new SmartPen.Pen(this);
      Pen.FingerSelected += Pen_FingerSelected;
      Pen.EraserSelected += Pen_EraserSelected;
      Pen.PenSelected += Pen_PenSelected;
    }

    public void SetText(string text)
    {
        Value = text;
        if(CountOfLabel.Dispatcher.CheckAccess())
        {
           // The calling thread owns the dispatcher, and hence the UI element
           CountOfLabel.Text = text;
        }
        else
        {
           // Invokation required
            CountOfLabel.Dispatcher.Invoke(DispatcherPriority.Normal, new Action<string>(SetText),  text );
        }
    }

    void Pen_PenSelected(object sender, SmartPen.Pen.PenEventArgs args)
    {
        if (sender == this)
        {
            SetText("Pen");
        }
    }

    void Pen_EraserSelected(object sender, SmartPen.Pen.PenEventArgs args)
    {
        if (sender == this)
        {
            SetText("Eraser");
        }
    }

    void Pen_FingerSelected(object sender, SmartPen.Pen.PenEventArgs args)
    {
        if (sender == this)
        {
            SetText("Finger");
        }
    }

    private void UpdateControls()
    {
        CountOfLabel.Text = string.Format("{0}={1}", FieldName, Value);
    }

    #region IWidget Members

    private string _caption = "";
    /// <summary>
    /// The text that is displayed in the widget's containing window title bar. This property is set during widget configuration.
    /// </summary>
    [DataMember(Name = "caption")]
    public string Caption
    {
      get
      {
        return _caption;
      }

      set
      {
        if (value != _caption)
        {
          _caption = value;
        }
      }
    }

    /// <summary>
    /// The unique identifier of the widget, set by the application when the widget is added to the configuration.
    /// </summary>
    [DataMember(Name = "id")]
    public string Id { get; set; }

    /// <summary>
    /// OnActivated is called when the widget is first added to the configuration, or when loading from a saved configuration, after all 
    /// widgets have been restored. Saved properties can be retrieved, including properties from other widgets.
    /// Note that some widgets may have properties which are set asynchronously and are not yet available.
    /// </summary>
    public void OnActivated()
    {
      UpdateControls();
    }

    /// <summary>
    ///  OnDeactivated is called before the widget is removed from the configuration.
    /// </summary>
    public void OnDeactivated()
    {
    }

    /// <summary>
    ///  Determines if the Configure method is called after the widget is created, before it is added to the configuration. Provides an opportunity to gather user-defined settings.
    /// </summary>
    /// <value>Return true if the Configure method should be called, otherwise return false.</value>
    public bool CanConfigure
    {
      get { return true; }
    }

    /// <summary>
    ///  Provides functionality for the widget to be configured by the end user through a dialog.
    /// </summary>
    /// <param name="owner">The application window which should be the owner of the dialog.</param>
    /// <param name="dataSources">The complete list of DataSources in the configuration.</param>
    /// <returns>True if the user clicks ok, otherwise false.</returns>
    public bool Configure(Window owner, IList<DataSource> dataSources)
    {
      // Show the configuration dialog.
      Config.CountWidgetDialog dialog = new Config.CountWidgetDialog(dataSources, Caption, DataSourceId, FieldName, Value) { Owner = owner };
      if (dialog.ShowDialog() != true)
        return false;

      // Retrieve the selected values for the properties from the configuration dialog.
      Caption = dialog.Caption;
      DataSourceId = dialog.DataSource.Id;
      Field = dialog.Field;
      FieldName = Field.Name;
      Value = dialog.Value;

      // Re-set the QueryValue after configuration, it will be set again for the new value
      // in the DoQuery method.
      QueryValue = null;

      // The default UI simply shows the values of the configured properties.
      UpdateControls();

      return true;

    }

    #endregion

    #region IDataSourceConsumer Members

    /// <summary>
    /// Returns the ID(s) of the data source(s) consumed by the widget.
    /// </summary>
    public string[] DataSourceIds
    {
      get { return new string[] { DataSourceId }; }
    }

    /// <summary>
    /// Called when a DataSource is removed from the configuration. 
    /// </summary>
    /// <param name="dataSource">The DataSource being removed.</param>
    public void OnRemove(DataSource dataSource)
    {
      // Respond to data source being removed. Setting the DataSourceId to null means that the 
      // OnRefresh will not be called again for that data source.
      DataSourceId = null;
      CountBlock.Text = "No Data";
    }

    /// <summary>
    /// Called when a DataSource found in the DataSourceIds property is updated.
    /// </summary>
    /// <param name="dataSource">The DataSource being updated.</param>
    public void OnRefresh(DataSource dataSource)
    {
      // Respond to the update from the selected data source using an async method to perform the query.
      DoQuery(dataSource);
    }

    private async void DoQuery(DataSource ds)
    {
      // If the widget is deserialized from saved values, make sure the Field is set from the FieldName.
      if ((Field == null) && (! string.IsNullOrEmpty(FieldName)))
      {
        Field =  ds.Fields.FirstOrDefault(fld => fld.Name == FieldName);
      }

      // Get a query value that takes into account coded value domains.
      if (string.IsNullOrEmpty(QueryValue))
      {
        QueryValue = GetQueryValue(ds);
      }

      // Perform a statistics query on the data source to get the count of requested features.
      Query countQuery = new Query(string.Format("{0} = {1}", FieldName, QueryValue), new string[] { FieldName });
      QueryResult result = await ds.ExecuteQueryStatisticAsync(Statistic.Count, countQuery);

      // Check the returned results.
      if ((result != null) && (result.Features != null) && (result.Features.Count == 1))
      {
        object val = result.Features[0].Attributes["Count"];
        if (val != null)
        {
          CountBlock.Text = val.ToString();
          return;
        }
      }

      // If no results were returned, clear the count box.
      CountBlock.Text = "No Data";
    }

    private string GetQueryValue(DataSource ds)
    {
      // Check for coded value domains
      if ((Field != null) && (Field.Domain != null) && (Field.Domain is client.FeatureService.CodedValueDomain))
      {
        // Translate coded value domain value into appropriate key to use in the query.
        client.FeatureService.CodedValueDomain codedDomain = Field.Domain as client.FeatureService.CodedValueDomain;
        KeyValuePair<object,string> valueCode = codedDomain.CodedValues.FirstOrDefault(val => val.Value == Value);
        string codedForValue = valueCode.Key.ToString();
        return codedForValue;
      }

      return Value;
    }

    #endregion

  }
}
