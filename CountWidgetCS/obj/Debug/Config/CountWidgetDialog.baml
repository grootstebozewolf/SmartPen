   M S B A M L   `   `   `  ����g  cESRI.ArcGIS.OperationsDashboard, Version=10.1.1.0, Culture=neutral, PublicKeyToken=8fc3cc631e44ad86zVclr-namespace:ESRI.ArcGIS.OperationsDashboard;assembly=ESRI.ArcGIS.OperationsDashboardESRI.ArcGIS.OperationsDashboard  5   	   �_clr-namespace:ESRI.ArcGIS.OperationsDashboard.Controls;assembly=ESRI.ArcGIS.OperationsDashboard(ESRI.ArcGIS.OperationsDashboard.Controls  5   	    CountWidgetCS,   &CountWidgetCS.Config.CountWidgetDialog   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   oopsDashboardVclr-namespace:ESRI.ArcGIS.OperationsDashboard;assembly=ESRI.ArcGIS.OperationsDashboard     5   	   {opsDashboardCtl_clr-namespace:ESRI.ArcGIS.OperationsDashboard.Controls;assembly=ESRI.ArcGIS.OperationsDashboard     5   	      ModalDialogWindowStyle#��[  5   	     � WindowStartupLocation$  CenterOwner=�5   	   $	��400��5   	   $	��350��5	   	    � Title$! Configure CountWidgetDialog��5
   	   .��5      �   DialogBackgroundBrush#���  6	    � RowDefinitions 5      �� 5      $��* �6   �� 5      $
��auto �6   .+�5      � $��10q�6    5      �� 5   
   $
��auto �6   �� 5   
   $
��auto �6   �� 5   
   $
��auto �6   �� 5   
   $
��auto �6    � ColumnDefinitions 5      �� 5   
   $
��auto �6   �� 5   
   $��* �6   .+�5      �� $��Title:��6     SettingLabelStyle#��[ 6    �� 5       -   5         ��Name$ CaptionTextBox��5       $��1��6(   $��
New Widget��68   $��5q�6J   �� 5"      $��1��6   $��Data Source:��6   #��[ 63   A   ;ESRI.ArcGIS.OperationsDashboard.Controls.DataSourceSelector  5#      -   5          Name$ DataSourceSelector��5#   +   $��1��5$   +   $��1��5%   +   5&   d   �� 5(      $��2��6   $��Field:��6   #��[ 6-   �� 5)      -   5         ��Name$ FieldComboBox��5)      $��2��6(   $��1��65    �� DisplayMemberPath$ Alias��6E   $��5q�6_   �� 5+      $��3��6   $��Value:��6   #��[ 6-   $��Center=�6X   �� 5,      -   5        $ ValueBox��5,      $��3��6"   $��1��6/   $��0��6?   $��5q�6H   5.      �� 50      $��1��6     DialogFooterBorderStyle#���  6   ._�51      �� $��Bottom=�6    �� Orientation$ 
Horizontal=�6.   $��Right=�6G   .+�52   
   �� -   5        	 ��Name$	 OKButton��52      $��65��6#   $	��5,0q�61   $��25��6?   $��5,5,0,5q�6K   $��OK��6\   6i   �� 53   
   $��65��6   $	��5,0q�6   $��25��6-   $��5q�69   $��Cancel��6D   
 �� IsCancel
 . 6U   54   	   55      57      58      