   M S B A M L   `   `   `  ����@  <TandA, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:TandA.ViewsTandA.Views  5   	   j fSyncfusion.SfBusyIndicator.WPF, Version=15.3451.0.33, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89�^clr-namespace:Syncfusion.Windows.Controls.Notification;assembly=Syncfusion.SfBusyIndicator.WPF(Syncfusion.Windows.Controls.Notification 5	   	   "    TandA.Views.EmployeeListVIew   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:TandA.Views     5   	   a ]Syncfusion.SfGrid.WPF, Version=15.3451.0.33, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89j fSyncfusion.SfGridConverter.WPF, Version=15.3451.0.33, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89d `Syncfusion.SfHubTile.Wpf, Version=15.3451.0.33, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89`	 \Syncfusion.Tools.Wpf, Version=15.3451.0.33, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89a
 ]Syncfusion.Shared.Wpf, Version=15.3451.0.33, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89<
syncfusion!http://schemas.syncfusion.com/wpf    	  
 5   	   {Notification^clr-namespace:Syncfusion.Windows.Controls.Notification;assembly=Syncfusion.SfBusyIndicator.WPF    	  
 5	   	   $	��700��5
   '   $	��650��64     � Title  6	   �� *WindowTitle+.��5      �  � RowDefinitions 5   
   �� 5      $
��auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   �� $��0��6   .+�5      �� .+�5      �� 
����d�._�5      �� .+�5      �� $��Employees Master��6$   
�������$��30(�6O   $��5 10 0 0q�6]   �� 5      $��List of all Employees��6$   
�������$��15(�6T   $��5 0 0 10q�6b   5      5      5      �� 5       �� Orientation$ 
Horizontal=�6   $
��10 5q�62   .+�5      �� $	��100��6   �� 5      
��� � �$��80��6,   
�������$��Columns��6J   $��10 0 0 0q�6\   �� 5      
��� � �$��80��6,   
�������$��Sorting��6J   $��10 0 0 0q�6\   �� 5      
��� � �$��80��6,   
�������$��Export��6J   $��10 0 0 0q�6[   �� 5       
��� � �$	��100��6,   
�������$��Advanced Search��6K   $��10 0 0 0q�6e   5!      5"      &   System.Windows.Controls.DataGrid  5#   
   -   5          Name$ dataGrid��5#         SelectionMode$ Single=�6%   $��1��6<      AutoGenerateColumns .     SelectionUnit$ FullRow=�6e   ��6~   �� *	Employees+ �� SelectedItem 6�   �� *
Employee+   	CellStyle 5%      �� 5&      	 �� 
TargetType*  $System.Windows.Controls.DataGridCell"	  6   5(      
   Columns
 5+      4  .System.Windows.Controls.DataGridTemplateColumn  5,      ,  &System.Windows.Controls.DataGridColumn   CanUserSort .    CellTemplate 5-      �� 5.      .R�5/      �� $
��Edit��6%   
����d�$��50��6�   ��64   �� *DataContext.EditEmployee+ �� RelativeSource �� *FindAncestor+ �� AncestorType" �6�   50      52      0  *System.Windows.Controls.DataGridTextColumn  53         Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6%      Header$ EMP ID��6R   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6/   �� *EmployeeNumber+6R     54      $ 2* 6%   $ 	LAST NAME��6M    60   �� *
Lastname+6M     55      $ 2* 6%   $ 
FIRST NAME��6N    60   �� *	Firstname+6N     56      $ 2* 6%   $ EMAIL ADDRESS��6Q    60   �� *EmailAddress+6Q   58      � 5?   
   $��3��6<   y�6   �� *IsEditEmployeeVisible+.+�5@      � 
���   � J� Opacity$	 0.2Q�6&   $��0,0,0,0q�64   �� 5A      $	��310��6   
����������#606366$��2q�6N   $��Center=�6b   $	��Top=�6   $
��0 70q�6�   ._�5B      � 
������� 5C      �� 5D      $
��Auto �6(   .+�5F      �� $��0��6!   .+�5G      �� $��Center=�6%   $
��0 10q�6B   .+�5H      �� $��Edit Employee��6(   $��20(�6=   5I      �� 5J      $ 
Horizontal=�6%   $	��5 5q�6>   .+�5K      �� $��Employee Number: ��6(   $	��120��6A   �� 5L      $	��100��6&   ��62   �� *EmployeeNumber+5M      �� 5O      $ 
Horizontal=�6%   $	��5 5q�6>   .+�5P      �� $��
Password: ��6(   $	��120��6:   G� 5Q      $	��100��6*   5R      �� 5T      $ 
Horizontal=�6%   $	��5 5q�6>   .+�5U      �� $��First Name: ��6(   $	��120��6<   �� 5V      $	��150��6&   ��62   �� *	Firstname+5W      �� 5Y      $ 
Horizontal=�6%   $	��5 5q�6>   .+�5Z      �� $��Last Name: ��6(   $	��120��6;   �� 5[      $	��150��6&   ��62   �� *
Lastname+5\      �� 5^      $ 
Horizontal=�6%   $	��5 5q�6>   .+�5_      �� $��Email Address: ��6(   $	��120��6?   �� 5`      $	��150��6&   ��62   �� *EmailAddress+5a      �� 5c      $ 
Horizontal=�6%   $	��5 5q�6>   .+�5d      �� $��Employee Groups��6(   $	��120��6?   �� 5e      $	��0 5q�6'   .��5g   "   �� $
��Auto=�6/   .��5h   &   �� ��63   �� *EmployeeGroups+��5i   *   �� 5j   .   .R�5k   2   �� $	��5 5q�6;    d� 	IsChecked 6H   �� *BelongsToGroup+��6m   �� *
GroupRef+5l   /   5n   '   5o   #   5p      5q      �� 5s      $ 
Horizontal=�6%   $
��5 15q�6>   .+�5t      �� $��80��6%   $
��10 0q�60   $
��Save��6>   ��6M   �� *CreateEmployee+��6p   ��  �� ElementName$ Groups�� �� Path$ SelectedItems��� 5u      $��80��6%   $
��10 0q�60   $��Delete��6>   ��6O   �� *CreateEmployee+��6r   �� $ Groups��$ SelectedItems��� 5v      $��80��6%   $
��10 0q�60   $
��Exit��6>   ��6M   �� *CloseEditEmployee+5w      5y      � 5z      
�������$	 0.7Q�6.   $��1��6j   y�6<   �� *WindowLoaderVisibility+.+�5{      >  8Syncfusion.Windows.Controls.Notification.SfBusyIndicator  $��1��67      AnimationType$ Sunny=�6H      Header$ 
Loading...��6^   5|      5~      5      5�      5�      5�      