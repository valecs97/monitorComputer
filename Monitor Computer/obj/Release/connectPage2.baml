   M S B A M L   `   `   `  ���� T  PMahApps.Metro, Version=1.2.4.0, Culture=neutral, PublicKeyToken=f4fb5a3c4d1e5b4fV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls  J FMonitorComputer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:Monitor_ComputerMonitor_Computer #   Monitor_Computer.connectPage2   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     PControls;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro     0localclr-namespace:Monitor_Computer       N� Title$  connectPage2��.��� .+��� -    ��Name$ 
backButton��$
��Left=�$��	10,10,0,0q�$	��Top=�$��40��   MetroCircleButtonStyle#���   $��40��.���� $��12��$��20������  �� RelativeSource ��  �� AncestorType$ ButtonL� �� Path$ 
Foreground� J� OpacityMask !�  j� Stretch$ Uniform=�  appbar_navigate_previous !� Visual# [ �� -   $ 
nextButton��$
��Left=�$��
583,10,0,0q�$	��Top=�$��40��#���   $��40��.���� $��12��$��20������  �� $ ButtonL�$ 
Foreground� !� $ Uniform=�  appbar_navigate_next# [ �� -    ��Name$ 	ipTextBox��$
��Left=�$��20��$��200,111,0,0q�	 �� TextWrapping$	 NoWrap=�$	��Top=�$	��171��
 �� 	MaxLength$
 40���� -   $ portTextBox��$
��Left=�$��20��$��371,111,0,0q�$	 NoWrap=�$	��Top=�$��60��$
 5���� -    ��Name$ ipLabel��$��IP��$
��Left=�$��
200,80,0,0q�$	��Top=��� -   $ 	portLabel��$
��Port��$
��Left=�$��
371,80,0,0q�$	��Top=�G� -    G�Name$ passwordTextBox��$
��Left=�$��23��$��200,142,0,0q�$	��Top=�$	��171�� G� PasswordChar$ *���� -   $ ipPortLabel��$��IP adress :��$
��Left=�$��137,111,0,0q�$	��Top=��� -	   $ passwordLabel��$��
Password :��$
��Left=�$��135,142,0,0q�$	��Top=��� -
   $ setHomeButton��$
��Left=�$��
10,207,0,0q�$	��Top=�$	��149��$��65��$��20(�.���� .+��� -    ��Name$ SetHomeTextButton1��$	��Set��$��Center=��� -   $ SetHomeTextButton2��$
��Home��$��Center=��� -   $ connectHomeButton��$
��Left=�$��244,207,0,0q�$	��Top=�$	��149��$��65��$��20(�.���� .+��� -   $ ConnectHomeTextButton1��$��Connect��$��Center=��� -   $ ConnectHomeTextButton2��$
��Home��$��Center=��� -   $ connectLastIpButton��$
��Left=�$��473,207,0,0q�$	��Top=�$	��149��$��65��$��20(�.���� .+��� -   $ connectLastIpTextButton1��$��Connect��$��Center=��� -   $ connectLastIpTextButton2��$��Last Ip��$��Center=��� -   $ 
helpButton��#���   $
��Left=�$��
295,10,0,0q�$	��Top=�$��40��$��40��$��20(�.���� $��12��$��20������  �� $ ButtonL�$ 
Foreground� !� $ Uniform=�  appbar_question# [ �� -   $ label��$1��+*leave blank if the server is password free��$
��Left=�$��376,142,0,0q�$	��Top=�$	��233��