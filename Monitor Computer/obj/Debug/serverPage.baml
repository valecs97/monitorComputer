   M S B A M L   `   `   `  ����T  PMahApps.Metro, Version=1.2.4.0, Culture=neutral, PublicKeyToken=f4fb5a3c4d1e5b4fV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls  5      J FMonitorComputer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:Monitor_ComputerMonitor_Computer 5      !   Monitor_Computer.serverPage   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      PControls;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      0localclr-namespace:Monitor_Computer     5        N� Title$  
serverPage��5
      .��5      � .+�5   
   �� -   5         ��Name$ 
backButton��5      $
��Left=�6%   $��	10,10,0,0q�6@   $	��Top=�6S   $��40��6k      MetroCircleButtonStyle#���   6v   $��40��6�   .��5      �� $��12��6   $��20��5      ��5      ��  �� RelativeSource ��  �� AncestorType$ ButtonL� �� Path$ 
Foreground� J� OpacityMask 5      !� 5       j� Stretch$ Uniform=�6"     appbar_navigate_previous !� Visual# [ 5   &   5      5      �� 5   
   -   5         ��Name$ portTextBox��5      $
��Left=�6'   $��20��6B   $��200,111,0,0q�6N   	 �� TextWrapping$	 NoWrap=�6c   $	��Top=�6z   $��60��6�   
 �� 	MaxLength$
 5��6�   6�   G� 5   
   -   5         G�Name$ passwordTextBox��5      $
��Left=�6/   $��23��6J   $��200,142,0,0q�6V   $	��Top=�6k   $	��171��6�    G� PasswordChar$ *��6�   �� 5   
   -   5         ��Name$ ipPortLabel��5      $��Port :��6%   $
��Left=�66   $��163,111,0,0q�6Q   $	��Top=�6f   �� 5   
   -   5        $ passwordLabel��5      $��
Password :��6'   $
��Left=�6<   $��135,142,0,0q�6W   $	��Top=�6l   �� 5   
   -   5        $ label��5      $1��+*leave blank if the server is password free��6   $
��Left=�6U   $��376,142,0,0q�6p   $	��Top=�6�   $	��233��6�   �� 5   
   -   5         ��Name$ bootUpCheckBox��5      $��start on computer bootup��6+   $
��Left=�6N   $��
10,254,0,0q�6i   $	��Top=�6}   6�   �� 5   
   -   5        $ keepServerOnCheckBox��5      $.��(keep server on even after the user exits��61   $
��Left=�6d   $��
10,231,0,0q�6   $	��Top=�6�   6�   �� 5   
   -	   5        $ startServerButton��5      $��Start Server��6,   $
��Left=�6C   $��457,203,0,0q�6^   $	��Top=�6s   $	��165��6�   $��69��6�   $��20(�6�   $��Segoe UI Black)�6�   6�   �� 5   
   -
   5        $ helpButtonServer��5      #���   6+   $
��Left=�6\   $��
295,10,0,0q�6w   $	��Top=�6�   $��40��6�   $��40��6�   $��20(�6�   .��5       �� $��12��6   $��20��5!      ��5"      �� *
Foreground+ �� " �� 5#      !� 5$      $ Uniform=�6"     appbar_question# [ 5%      5'      5(      5)      5*      