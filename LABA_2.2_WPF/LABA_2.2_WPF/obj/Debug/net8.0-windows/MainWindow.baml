   M S B A M L   `   `   `  ����G  CLABA_2.2_WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:LABA_2._2_WPFLABA_2._2_WPF  5   	       LABA_2._2_WPF.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:LABA_2._2_WPF     5   	     � Title$  
MainWindow��5   	   $	��600��6   $	��800��6)   .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   `� $��0��6   .�5      c� .@�5      �� $
��Play��69   ��6   �� *PlayCommand+69   �� 5      $��	Open File��6=   ��6   �� *OpenFileCommand+6=   5      5      �� 5   
   $��1��6    �� AcceptsReturn$
 True��6Z   ��6   �� *Code+ �� UpdateSourceTrigger$ PropertyChanged=�6Z   �� 5   
   $��2��6   .@�5      �� ��6   �� *StatusMessage+5      5      5       