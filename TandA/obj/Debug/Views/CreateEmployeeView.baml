   M S B A M L   `   `   `  ����@  <TandA, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:TandA.ViewsTandA.Views  5   	   j fSyncfusion.SfBusyIndicator.WPF, Version=15.3451.0.33, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89�^clr-namespace:Syncfusion.Windows.Controls.Notification;assembly=Syncfusion.SfBusyIndicator.WPF(Syncfusion.Windows.Controls.Notification 5	   
   $    TandA.Views.CreateEmployeeView   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:TandA.Views     5   	   a ]Syncfusion.SfGrid.WPF, Version=15.3451.0.33, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89j fSyncfusion.SfGridConverter.WPF, Version=15.3451.0.33, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89d `Syncfusion.SfHubTile.Wpf, Version=15.3451.0.33, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89`	 \Syncfusion.Tools.Wpf, Version=15.3451.0.33, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89a
 ]Syncfusion.Shared.Wpf, Version=15.3451.0.33, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89<
syncfusion!http://schemas.syncfusion.com/wpf    	  
 5   	   {Notification^clr-namespace:Syncfusion.Windows.Controls.Notification;assembly=Syncfusion.SfBusyIndicator.WPF    	  
 5	   
   $	��300��5
   '   $	��300��64     � Title  6	   �� *WindowTitle+.��5      � 
������� � RowDefinitions 5   
   �� 5      $��* �6   .+�5   
   �� $��0��6   .+�5      �� $��Center=�6   $
��0 10q�66   .+�5      �� $��Create Employee��6   $��20(�63   5      �� 5       �� Orientation$ 
Horizontal=�6   $	��5 5q�62   .+�5      �� $��Employee Number: ��6   $	��120��65   �� 5      $	��100��6   ��6&   �� *EmployeeNumber+5      �� 5      $ 
Horizontal=�6   $	��5 5q�62   .+�5      �� $��
Password: ��6   $	��120��6.   G� 5      -   5        $	��100��5      6*   5      �� 5      $ 
Horizontal=�6   $	��5 5q�62   .+�5      �� $��First Name: ��6   $	��120��60   �� 5      $	��150��6   ��6&   �� *	Firstname+5       �� 5"      $ 
Horizontal=�6   $	��5 5q�62   .+�5#      �� $��Last Name: ��6   $	��120��6/   �� 5$      $	��150��6   ��6&   �� *
Lastname+5%      �� 5'      $ 
Horizontal=�6   $	��5 5q�62   .+�5(      �� $��Email Address: ��6   $	��120��63   �� 5)      $	��150��6   ��6&   �� *EmailAddress+5*      �� 5,      $ 
Horizontal=�6   $	��5 5q�62   .+�5-      �� $��Group: ��6   $	��120��6+   3 
 -Syncfusion.Windows.Tools.Controls.ComboBoxAdv  5.      -   5          Name$ Groups��5.   )    �� DisplayMemberPath$ GroupRef��69      DefaultText$ Select Group��6V      AllowMultiSelect . $	��150��6�   ��6�   �� *Groups+   SelectedItems 6�   �� *SelectedItems+ �� UpdateSourceTrigger$ PropertyChanged=�6  5/      �� 51      $ 
Horizontal=�6   $
��5 15q�62   .+�52      �� $��80��6   $
��40 0q�6$   $��Create��62   ��6C   �� *CreateEmployee+��6f   �� 	 �� ElementName$	 Groups��
 �� Path$
 SelectedItems��� 53      -   5        $��80��53      $
��10 0q�6$   $
��Exit��62   6A   54      56      � 57   
   
������� J� Opacity$	 0.7Q�6"   $��1��6^   y�60   �� *WindowLoaderVisibility+.+�58      >  8Syncfusion.Windows.Controls.Notification.SfBusyIndicator  $��1��6+      AnimationType$ Sunny=�6<      Header$ 
Loading...��6R   59      5;      5<      