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
using ESRI.ArcGIS.OperationsDashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using client = ESRI.ArcGIS.Client;

namespace CountWidgetCS.Config
{
  /// <summary>
  /// Interaction logic for CountWIdgetDialog.xaml
  /// </summary>
  public partial class CountWidgetDialog : Window
  {

    public DataSource DataSource { get; private set; }
    public ESRI.ArcGIS.Client.Field Field { get; private set; }
    public string Caption { get; private set; }
    public string Value { get; private set; }

    public CountWidgetDialog(IList<DataSource> dataSources, string initialCaption, string initialDataSourceId, string initialField, string initialValue)
    {
      InitializeComponent();

      // When re-configuring, initialize the widget config dialog from the existing settings.
      CaptionTextBox.Text = initialCaption;
      ValueBox.Text = initialValue;

      if (!string.IsNullOrEmpty(initialDataSourceId))
      {
        DataSource dataSource = OperationsDashboard.Instance.DataSources.FirstOrDefault(ds => ds.Id == initialDataSourceId);
        if (dataSource != null)
        {
          DataSourceSelector.SelectedDataSource = dataSource;
          if (!string.IsNullOrEmpty(initialField))
          {
            client.Field field = dataSource.Fields.FirstOrDefault(fld => fld.FieldName == initialField);
            FieldComboBox.SelectedItem = field;
          }
        }
      }
    }


    private void OKButton_Click(object sender, RoutedEventArgs e)
    {
      DataSource = DataSourceSelector.SelectedDataSource;
      Caption = CaptionTextBox.Text;
      Field = (ESRI.ArcGIS.Client.Field)FieldComboBox.SelectedItem;
      Value = ValueBox.Text;

      DialogResult = true;
    }

    private void DataSourceSelector_SelectionChanged(object sender, EventArgs e)
    {
      DataSource dataSource = DataSourceSelector.SelectedDataSource;
      FieldComboBox.ItemsSource = dataSource.Fields;
      FieldComboBox.SelectedItem = dataSource.Fields[0];
      List<ESRI.ArcGIS.Client.Field> numericFields = new List<ESRI.ArcGIS.Client.Field>();
      foreach (var field in dataSource.Fields)

        ValidateInput(sender, null);
    }

    private void ValidateInput(object sender, TextChangedEventArgs e)
    {
      if (OKButton == null)
        return;

      OKButton.IsEnabled = false;
      if (string.IsNullOrEmpty(CaptionTextBox.Text))
        return;

      OKButton.IsEnabled = true;
    }


  }
}
