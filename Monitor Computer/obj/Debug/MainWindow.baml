   M S B A M L   `   `   `  ����T  PMahApps.Metro, Version=1.2.4.0, Culture=neutral, PublicKeyToken=f4fb5a3c4d1e5b4fV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls  5   	   J FMonitorComputer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:Monitor_ComputerMonitor_Computer 5   	   3clr-namespace:Samples.CommandsSamples.Commands 5   	   !   Monitor_Computer.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   PControls;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro     5   	   0localclr-namespace:Monitor_Computer     5   	   X TXceed.Wpf.Toolkit, Version=2.7.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5	   	   \ XHardcodet.Wpf.TaskbarNotification, Version=1.0.5.0, Culture=neutral, PublicKeyToken=null)tb http://www.hardcodet.net/taskbar  5
   	   -commandsclr-namespace:Samples.Commands  5   	   -   5          � Title$  Computer Monitor��5   	   $	��360��6"   $	��640��6/   (   "MahApps.Metro.Controls.MetroWindow   ShowMaxRestoreButton .   � 
ResizeMode$ CanMinimize=�5   	    � Icon$) #Resources/Computer_Monitor_Logo.ico��5   	      AccentColorBrush#���   5   	   $��1q�5   	    � WindowStartupLocation$ CenterScreen=�5   	      RightWindowCommands 5      +   %MahApps.Metro.Controls.WindowCommands  5   
   .@�5      �� -   5         ��Name$ speedControl��5      $��	Speed : -��6+   �� 5      -   5        $ statusControl��5      $��Status : offline��6,   �� 5      -   5        .��5      ��  �� Orientation$ 
Horizontal=�6   .+�5      �� -   5         ��Name$ StatusChangeInfo��5      $��4 0 0 0q�5       $��Center=�5      $��Send Feedback��5       5!      5"      �� 5#      -   5        .��5$      �� $ 
Horizontal=�6   .+�5%      �� -   5        $ 	AboutInfo��5'      $��8 0 0 0q�5%       $��Center=�5&      $��About��5(      5*      5+      5,      .��51      � 	 � 	Resources	 52   
   + %Monitor_Computer.notificationCommands  MessageCommand��%!   &	         53      54      55      .+�56   
   3  -Hardcodet.Wpf.TaskbarNotification.TaskbarIcon  -   5        
  Name$
 notificationIcon��56         
IconSource$* $/Resources/Computer_Monitor_Logo.ico��63      ToolTipText$ Monitor Computer��6e    � ContextMenu 57      �� 58      .@�59      m� $
��Open��6    m� Command 5:      .  (Samples.Commands.ShowSampleWindowCommand   m� CommandParameter 5;      �� 5<      m� 5>      $
��Exit��6    5?      )  #Samples.Commands.CloseWindowCommand   5@      �� 5A      5B      5E      �� 5F   
   -	   5         ��Name$ 
tabControl��5F      $
��Left=�6*   $��0,2,0,0q�6E   $	��Top=�6V    J� RenderTransformOrigin$ 0.5,0.5&�6n   $	��632��6�   $	��317��6�   .@�5G      �� -
   5         ��Name$ settingsTab��5G   (   $��Settings��6   $ 0.794,0.462&�6=   $	��123��6a   .��5I      � $	��282��6   $	��Top=�6$   $��	0,0,0,-10q�6<   .+�5J      ]� .+�5K       � -   5          �Name$ connectWindow��5K         � NavigationUIVisibility$ Hidden=�67   5L      5M      5N      �� 5P      -   5        $ taskManagerTab��5P   /   $��Cpu/Ram Manager��6   $ 0.794,0.462&�6G   $	��245��6k   .��5R      � $	��282��6   $	��Top=�6$   $��	0,0,0,-10q�6<   .+�5S      � $��	0,-2,-4,0q�6   .+�5T      *   $MahApps.Metro.Controls.NumericUpDown  -   5          Name$ 
warningCPU��5T   1      Minimum$ 10Q�6E      Maximum$	 100Q�6R      Interval$ 1Q�6`   $��10,111,427,146q�6m      InterceptArrowKeys .    InterceptMouseWheel .    InterceptManualEnter .    Value$ 90X�6�      StringFormat$ {0:N0} %��6�   6�     5U      -   5        $ 
warningRAM��5U   1   $ 1Q�6E   $ 131072Q�6Q   $ 1Q�6b   $��10,174,427,83q�6o    .  .  . $	 512X�6�   $ 	{0:N0} MB��6�   6�     5V      -   5        $ warningTime��5V   1   $ 1Q�6F   $ 60Q�6R   $ 1Q�6_   $��10,237,427,20q�6l    .  .  . $ 60X�6�   $ {0:N0} s��6�   6�   �� 5W      -   5         ��Name$ MonitoringLabel1��5W       $'��!Warning when Cpu usage is above :��6:   $
��Left=�6f   $��	10,80,0,0q�6�   $	��Top=�6�   $	��205��6�   �� 5X      -   5        $ MonitoringLabel2��5X       $&�� Warning when free RAM is under :��6:   $
��Left=�6e   $��
10,143,0,0q�6�   $	��Top=�6�   $	��195��6�   �� 5Y      -   5        $ MonitoringLabel3��5Y       $��Warning after x seconds :��6:   $
��Left=�6^   $��
10,206,0,0q�6y   $	��Top=�6�   $	��195��6�   �� 5Z      -   5        $ MonitoringStatsCPU��5Z       $��Cpu usage : -��6<   $
��Left=�6T   $��	10,10,0,0q�6o   $	��Top=�6�   $��30��6�   $	��165��6�   �� 5[      -   5        $ MonitoringStatsRAM��5[       $��Available Ram : -��6<   $
��Left=�6X   $��	10,40,0,0q�6s   $	��Top=�6�   $��30��6�   $	��165��6�   5\      5]      5^      �� 5_      -   5        $ commandsTab��5_   (   $��Commands��6   $ 0.794,0.462&�6=   $	��160��6a   .��5`      � .+�5a      �� -   5        $ shutDownComputer��5a      $
��Left=�67   $	��Top=�6R   $	��165��6j   $��69��6v   $��20(�6�   $��Segoe UI Black)�6�   $��	10,36,0,0q�6�   .��5b      �� .+�5c      �� -   5        $  StartMonitoringTextButton3��5c   (   $��	Shut Down��6L   $��Center=�6]   5d      5e      �� 5f      -   5        $ screenShotButton��5f      $
��Left=�67   $	��Top=�6R   $	��165��6j   $��69��6v   $��20(�6�   $��Segoe UI Black)�6�   $��
193,36,0,0q�6�   .��5g      �� .+�5h      �� -   5        $ ScreenShotTextButton��5h   (   $��
Screenshot��6F   $��Center=�6X   5i      5j      � 5k      -   5         �Name$ progressBar��5k   "   $
��Left=�67   $��193,110,0,0q�6R   $	��Top=�6g   $	��165��6   $��15��6�   �� 5l      -   5        $ restartComputer��5l      $
��Left=�66   $	��Top=�6Q   $	��165��6i   $��69��6u   $��20(�6�   $��Segoe UI Black)�6�   $��
10,110,0,0q�6�   .��5m      �� .+�5n      �� -   5        $  StartMonitoringTextButton1��5n   (   $��Center=�6L   $��Restart��6i   5o      5p      �� 5q      -   5        $ 	setFolder��5q      $
��Left=�60   $	��Top=�6K   $	��165��6c   $��69��6o   $��20(�6{   $��Segoe UI Black)�6�   $��
10,184,0,0q�6�   .��5r      �� .+�5s      �� -   5        $ ScreenShotTextButton1��5s   (   $��Center=�6G   $��
Set Folder��6d   5t      5u      �� 5v      -   5        $ sendFileButton��5v      $
��Left=�65   $	��Top=�6P   $	��165��6h   $��69��6t   $��20(�6�   $��Segoe UI Black)�6�   $��
375,36,0,0q�6�   .��5w      �� .+�5x      �� -    5        $ ScreenShotTextButton2��5x   (   $��	Send file��6G   $��Center=�6X   5y      5z      � 5{      -!   5        $ progressBar2��5{   "   $
��Left=�68   $��375,110,0,0q�6S   $	��Top=�6h   $	��165��6�   $��15��6�   �� 5|      -"   5        $ helpButtonCommands��5|        MetroCircleButtonStyle#���  69   $
��Left=�6j   $��
578,10,0,0q�6�   $	��Top=�6�   $��40��6�   $��40��6�   $��20(�6�   .��5}      �� $��12��6$   $��20��5~      ��5      �� *
Foreground+  �� RelativeSource  �� ! �� AncestorType"! ��" J� OpacityMask" 5�      !� 5�   "   # j� Stretch$# Uniform=�6.     appbar_question$ !� Visual#$ [ 5�      5�      5�      �� 5�      -#   5        $ atentionButtonCommands��5�      #���  6=   $
��Left=�6n   $��
578,55,0,0q�6�   $	��Top=�6�   $��40��6�   $��40��6�   $��20(�6�   .��5�      �� $��12��6$   $��20��5�      ��5�      �� *
Foreground+  �� "! ��" 5�      !� 5�   "   $# Uniform=�6.     appbar_alert#$ [ 5�      5�      5�      5�      5�      �� 5�      $
��Help��6   .��5�      � $��
0,-3,0,-10q�6   .+�5�      �� -$   5        $ label��5�      $����Hello , it seems that you need a little help. 

Computer monitor will help you monitor your home computer from another one ( for example your laptop) . You 
can check your cpu/ram usage , you can shut down, restart or put your computer to sleep. If you want to check 
on your computer you can take a screenshot and see whats happening back home, also you can send any file 
to your home computer .

Anything else is self explaining , you go to settings to open the server or connect to one ... you can use the 
application functions only if you are connected to somebody. The moment you disconnect you can't access 
them anymore (for security reasons).��6+   $
��Left=�6�  $��	10,10,0,0q�6  $	��Top=�6(  $	��171��6@  $	��608��6M  5�      5�      5�      5�      5�      