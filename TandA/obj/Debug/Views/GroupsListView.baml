   M S B A M L   `   `   `  ����@  <TandA, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:TandA.ViewsTandA.Views  5   	   j fSyncfusion.SfBusyIndicator.WPF, Version=15.3451.0.33, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89�^clr-namespace:Syncfusion.Windows.Controls.Notification;assembly=Syncfusion.SfBusyIndicator.WPF(Syncfusion.Windows.Controls.Notification 5
   	        TandA.Views.GroupsListView   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:TandA.Views     5   	   a ]Syncfusion.SfGrid.WPF, Version=15.3451.0.33, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89j fSyncfusion.SfGridConverter.WPF, Version=15.3451.0.33, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89d `Syncfusion.SfHubTile.Wpf, Version=15.3451.0.33, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89`	 \Syncfusion.Tools.Wpf, Version=15.3451.0.33, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89a
 ]Syncfusion.Shared.Wpf, Version=15.3451.0.33, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89<
syncfusion!http://schemas.syncfusion.com/wpf    	  
 5   	   {Notification^clr-namespace:Syncfusion.Windows.Controls.Notification;assembly=Syncfusion.SfBusyIndicator.WPF    	  
 5
   	   $	��400��5   '   $	��570��64     � Title  6	   �� *WindowTitle+.��5      �  � RowDefinitions 5   
   �� 5      $
��auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   �� $��0��6   .+�5      �� .+�5      �� 
����d�._�5      �� .+�5      �� $��List Groups��6$   
�������$��30(�6J   $��5 10 0 0q�6X   �� 5      $��List of all Groups��6$   
�������$��15(�6Q   $��5 0 0 10q�6_   5      5      5      �� 5       �� Orientation$ 
Horizontal=�6   $
��10 5q�62   .+�5      �� $	��100��6   �� 5      
��� � �$��80��6,   
�������$��Columns��6J   $��10 0 0 0q�6\   �� 5      
��� � �$��80��6,   
�������$��Sorting��6J   $��10 0 0 0q�6\   �� 5       
��� � �$��80��6,   
�������$��Export��6J   $��10 0 0 0q�6[   �� 5!      
��� � �$	��100��6,   
�������$��Advanced Search��6K   $��10 0 0 0q�6e   5"      5#      &   System.Windows.Controls.DataGrid  5$   
   -   5          Name$ dataGrid��5$         SelectionMode$ Single=�6%   $��1��6<      AutoGenerateColumns .     SelectionUnit$ FullRow=�6e   ��6~   �� *Groups+ �� SelectedItem 6�   �� *Group+   	CellStyle 5&      �� 5'      	 �� 
TargetType*  $System.Windows.Controls.DataGridCell"	  6   5)      
   Columns
 5,      4  .System.Windows.Controls.DataGridTemplateColumn  5-      ,  &System.Windows.Controls.DataGridColumn   CanUserSort .    CellTemplate 5.      �� 5/      .R�50      �� $
��Edit��6%   
����d�$��50��6�   ��64   �� *DataContext.EditGroup+ �� RelativeSource �� *FindAncestor+ �� AncestorType" �6�   51      53      0  *System.Windows.Controls.DataGridTextColumn  54         Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6%      Header$ GROUP REFERENCE��6L   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6/   �� *
GroupRef+6L     55      $ 2* 6%   $ GROUP DESCRIPTION��6U    60   �� *GroupDescription+6U     56      $ * 6%   $ GROUP SUPERVISOR��6P    6/   �� *SupervisorNo+6P   58      � 5?   
   $��3��69   y�6   �� *IsEditGroupVisible+.+�5@      � 
���   � J� Opacity$	 0.2Q�6&   $��0,0,0,0q�64   �� 5A      $	��310��6   
����������#606366$��2q�6N   $��Center=�6b   $	��Top=�6   $
��0 70q�6�   ._�5B      � 
������� 5C      �� 5D      $
��Auto �6(   .+�5F      �� $��0��6!   .+�5G      �� $��Center=�6%   $
��0 10q�6B   .+�5H      �� $��
Edit Group��6(   $��20(�6:   5I      �� 5J      $ 
Horizontal=�6%   $	��5 5q�6>   .+�5K      �� $��Group Ref: ��6(   $	��120��6;   �� 5L      $	��100��6&    �� 
IsReadOnly . ��6D   �� *
GroupRef+5M      �� 5O      $ 
Horizontal=�6%   $	��5 5q�6>   .+�5P      �� $��Group Description: ��6(   $	��120��6C   �� 5Q      $	��150��6&   ��62   �� *	GroupDesc+5R      �� 5T      $ 
Horizontal=�6%   $	��5 5q�6>   .+�5U      �� $��Group Supervisor: ��6(   $	��120��6B   �� 5V      $	��150��6'   5W      �� 5Y      $ 
Horizontal=�6%   $
��5 15q�6>   .+�5Z      �� $��80��6%   $
��10 0q�60   $
��Save��6>   ��6M   �� *CreateEmployee+��6p   ��  �� ElementName$ Groups�� �� Path$ SelectedItems��� 5[      $��80��6%   $
��10 0q�60   $��Delete��6>   ��6O   �� *CreateEmployee+��6r   �� $ Groups��$ SelectedItems��� 5\      $��80��6%   $
��10 0q�60   $
��Exit��6>   ��6M   �� *CloseEditGroup+5]      5_      � 5`      
�������$	 0.7Q�6.   $��1��6j   y�6<   �� *WindowLoaderVisibility+.+�5a      >  8Syncfusion.Windows.Controls.Notification.SfBusyIndicator  $��1��67      AnimationType$ Sunny=�6H      Header$ 
Loading...��6^   5b      5d      5e      5f      5h      5i      