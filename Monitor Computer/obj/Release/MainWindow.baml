   M S B A M L   `   `   `  ���� T  PMahApps.Metro, Version=1.2.4.0, Culture=neutral, PublicKeyToken=f4fb5a3c4d1e5b4fV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls  J FMonitorComputer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:Monitor_ComputerMonitor_Computer 3clr-namespace:Samples.CommandsSamples.Commands !   Monitor_Computer.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     PControls;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro     0localclr-namespace:Monitor_Computer     X TXceed.Wpf.Toolkit, Version=2.7.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  \ XHardcodet.Wpf.TaskbarNotification, Version=1.0.5.0, Culture=neutral, PublicKeyToken=null)tb http://www.hardcodet.net/taskbar  -commandsclr-namespace:Samples.Commands  -     � Title$  Computer Monitor��$	��360��$	��640��(   "MahApps.Metro.Controls.MetroWindow   ShowMaxRestoreButton .   � 
ResizeMode$ CanMinimize=� � Icon$) #Resources/Computer_Monitor_Logo.ico��   AccentColorBrush#���   $��1q� � WindowStartupLocation$ CenterScreen=�   RightWindowCommands +   %MahApps.Metro.Controls.WindowCommands  .@��� -    ��Name$ speedControl��$��	Speed : -���� -   $ statusControl��$��Status : offline���� -   .����  �� Orientation$ 
Horizontal=�.+��� -    ��Name$ StatusChangeInfo��$��4 0 0 0q�$��Center=�$��Send Feedback���� -   .���� $ 
Horizontal=�.+��� -   $ 	AboutInfo��$��8 0 0 0q�$��Center=�$��About��.��� 	 � 	Resources	 + %Monitor_Computer.notificationCommands  MessageCommand��%   &	         .+�3  -Hardcodet.Wpf.TaskbarNotification.TaskbarIcon  -   
  Name$
 notificationIcon��   
IconSource$* $/Resources/Computer_Monitor_Logo.ico��   ToolTipText$ Monitor Computer�� � ContextMenu �� .@�m� $
��Open�� m� Command .  (Samples.Commands.ShowSampleWindowCommand   m� CommandParameter �� m� $
��Exit�� )  #Samples.Commands.CloseWindowCommand   �� �� -	    ��Name$ 
tabControl��$
��Left=�$��0,2,0,0q�$	��Top=� J� RenderTransformOrigin$ 0.5,0.5&�$	��632��$	��317��.@��� -
    ��Name$ settingsTab��$��Settings��$ 0.794,0.462&�$	��123��.��� $	��282��$	��Top=�$��	0,0,0,-10q�.+�]� .+� � -     �Name$ connectWindow��  � NavigationUIVisibility$ Hidden=��� -   $ taskManagerTab��$��Cpu/Ram Manager��$ 0.794,0.462&�$	��245��.��� $	��282��$	��Top=�$��	0,0,0,-10q�.+�� $��	0,-2,-4,0q�.+�*   $MahApps.Metro.Controls.NumericUpDown  -     Name$ 
warningCPU��   Minimum$ 10Q�   Maximum$	 100Q�   Interval$ 1Q�$��10,111,427,146q�   InterceptArrowKeys .    InterceptMouseWheel .    InterceptManualEnter .    Value$ 90X�   StringFormat$ {0:N0} %��  -   $ 
warningRAM��$ 1Q�$ 131072Q�$ 1Q�$��10,174,427,83q� .  .  . $	 512X�$ 	{0:N0} MB��  -   $ warningTime��$ 1Q�$ 60Q�$ 1Q�$��10,237,427,20q� .  .  . $ 60X�$ {0:N0} s���� -    ��Name$ MonitoringLabel1��$'��!Warning when Cpu usage is above :��$
��Left=�$��	10,80,0,0q�$	��Top=�$	��205���� -   $ MonitoringLabel2��$&�� Warning when free RAM is under :��$
��Left=�$��
10,143,0,0q�$	��Top=�$	��195���� -   $ MonitoringLabel3��$��Warning after x seconds :��$
��Left=�$��
10,206,0,0q�$	��Top=�$	��195���� -   $ MonitoringStatsCPU��$��Cpu usage : -��$
��Left=�$��	10,10,0,0q�$	��Top=�$��30��$	��165���� -   $ MonitoringStatsRAM��$��Available Ram : -��$
��Left=�$��	10,40,0,0q�$	��Top=�$��30��$	��165���� -   $ commandsTab��$��Commands��$ 0.794,0.462&�$	��160��.��� .+��� -   $ shutDownComputer��$
��Left=�$	��Top=�$	��165��$��69��$��20(�$��Segoe UI Black)�$��	10,36,0,0q�.���� .+��� -   $  StartMonitoringTextButton3��$��	Shut Down��$��Center=��� -   $ screenShotButton��$
��Left=�$	��Top=�$	��165��$��69��$��20(�$��Segoe UI Black)�$��
193,36,0,0q�.���� .+��� -   $ ScreenShotTextButton��$��
Screenshot��$��Center=�� -    �Name$ progressBar��$
��Left=�$��193,110,0,0q�$	��Top=�$	��165��$��15���� -   $ restartComputer��$
��Left=�$	��Top=�$	��165��$��69��$��20(�$��Segoe UI Black)�$��
10,110,0,0q�.���� .+��� -   $  StartMonitoringTextButton1��$��Center=�$��Restart���� -   $ 	setFolder��$
��Left=�$	��Top=�$	��165��$��69��$��20(�$��Segoe UI Black)�$��
10,184,0,0q�.���� .+��� -   $ ScreenShotTextButton1��$��Center=�$��
Set Folder���� -   $ sendFileButton��$
��Left=�$	��Top=�$	��165��$��69��$��20(�$��Segoe UI Black)�$��
375,36,0,0q�.���� .+��� -    $ ScreenShotTextButton2��$��	Send file��$��Center=�� -!   $ progressBar2��$
��Left=�$��375,110,0,0q�$	��Top=�$	��165��$��15���� -"   $ helpButtonCommands��  MetroCircleButtonStyle#���  $
��Left=�$��
578,10,0,0q�$	��Top=�$��40��$��40��$��20(�.���� $��12��$��20������ *
Foreground+  �� RelativeSource  �� ! �� AncestorType"! ��" J� OpacityMask" !� # j� Stretch$# Uniform=�  appbar_question$ !� Visual#$ [ �� -#   $ atentionButtonCommands��#���  $
��Left=�$��
578,55,0,0q�$	��Top=�$��40��$��40��$��20(�.���� $��12��$��20������ *
Foreground+  �� "! ��" !� $# Uniform=�  appbar_alert#$ [ �� $
��Help��.��� $��
0,-3,0,-10q�.+��� -$   $ label��$����Hello , it seems that you need a little help. 

Computer monitor will help you monitor your home computer from another one ( for example your laptop) . You 
can check your cpu/ram usage , you can shut down, restart or put your computer to sleep. If you want to check 
on your computer you can take a screenshot and see whats happening back home, also you can send any file 
to your home computer .

Anything else is self explaining , you go to settings to open the server or connect to one ... you can use the 
application functions only if you are connected to somebody. The moment you disconnect you can't access 
them anymore (for security reasons).��$
��Left=�$��	10,10,0,0q�$	��Top=�$	��171��$	��608��