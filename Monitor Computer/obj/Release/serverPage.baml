   M S B A M L   `   `   `  ���� T  PMahApps.Metro, Version=1.2.4.0, Culture=neutral, PublicKeyToken=f4fb5a3c4d1e5b4fV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls  J FMonitorComputer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:Monitor_ComputerMonitor_Computer !   Monitor_Computer.serverPage   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     PControls;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro     @d2http://schemas.microsoft.com/expression/blend/2008     0localclr-namespace:Monitor_Computer       N� Title$  
serverPage��.��� .+��� -    ��Name$ 
backButton��$
��Left=�$��	10,10,0,0q�$	��Top=�$��40��   MetroCircleButtonStyle#���   $��40��.���� $��12��$��20������  �� RelativeSource ��  �� AncestorType$ ButtonL� �� Path$ 
Foreground� J� OpacityMask !�  j� Stretch$ Uniform=�  appbar_navigate_previous !� Visual# [ �� -    ��Name$ portTextBox��$
��Left=�$��20��$��200,111,0,0q�	 �� TextWrapping$	 NoWrap=�$	��Top=�$��60��
 �� 	MaxLength$
 5��G� -    G�Name$ passwordTextBox��$
��Left=�$��23��$��200,142,0,0q�$	��Top=�$	��171�� G� PasswordChar$ *���� -    ��Name$ ipPortLabel��$��Port :��$
��Left=�$��163,111,0,0q�$	��Top=��� -   $ passwordLabel��$��
Password :��$
��Left=�$��135,142,0,0q�$	��Top=��� -   $ label��$1��+*leave blank if the server is password free��$
��Left=�$��376,142,0,0q�$	��Top=�$	��233���� -    ��Name$ bootUpCheckBox��$��start on computer bootup��$
��Left=�$��
10,254,0,0q�$	��Top=��� -   $ keepServerOnCheckBox��$.��(keep server on even after the user exits��$
��Left=�$��
10,231,0,0q�$	��Top=��� -	   $ startServerButton��$��Start Server��$
��Left=�$��457,203,0,0q�$	��Top=�$	��165��$��69��$��20(�$��Segoe UI Black)��� -
   $ helpButtonServer��#���   $
��Left=�$��
295,10,0,0q�$	��Top=�$��40��$��40��$��20(�.���� $��12��$��20������ *
Foreground+ �� " �� !� $ Uniform=�  appbar_question# [ 