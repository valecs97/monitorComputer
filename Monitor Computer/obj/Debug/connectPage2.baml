   M S B A M L   `   `   `  ����T  PMahApps.Metro, Version=1.2.4.0, Culture=neutral, PublicKeyToken=f4fb5a3c4d1e5b4fV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls  5      J FMonitorComputer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:Monitor_ComputerMonitor_Computer 5      #   Monitor_Computer.connectPage2   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      PControls;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro     5      0localclr-namespace:Monitor_Computer     5        N� Title$  connectPage2��5
      .��5      � .+�5   
   �� -   5         ��Name$ 
backButton��5      $
��Left=�6%   $��	10,10,0,0q�6@   $	��Top=�6S   $��40��6k      MetroCircleButtonStyle#���   6v   $��40��6�   .��5      �� $��12��6   $��20��5      ��5      ��  �� RelativeSource ��  �� AncestorType$ ButtonL� �� Path$ 
Foreground� J� OpacityMask 5      !� 5       j� Stretch$ Uniform=�6"     appbar_navigate_previous !� Visual# [ 5   &   5      5      �� 5   
   -   5        $ 
nextButton��5      $
��Left=�6%   $��
583,10,0,0q�6@   $	��Top=�6T   $��40��6l   #���   6w   $��40��6�   .��5      �� $��12��6   $��20��5      ��5      ��  �� $ ButtonL�$ 
Foreground� 5      !� 5      $ Uniform=�6"     appbar_navigate_next# [ 5   &   5      5      �� 5    
   -   5         ��Name$ 	ipTextBox��5       $
��Left=�6%   $��20��6@   $��200,111,0,0q�6L   	 �� TextWrapping$	 NoWrap=�6a   $	��Top=�6w   $	��171��6�   
 �� 	MaxLength$
 40��6�   �� 5!   
   -   5        $ portTextBox��5!      $
��Left=�6'   $��20��6B   $��371,111,0,0q�6N   $	 NoWrap=�6c   $	��Top=�6z   $��60��6�   $
 5��6�   6�   �� 5"   
   -   5         ��Name$ ipLabel��5"      $��IP��6!   $
��Left=�6.   $��
200,80,0,0q�6I   $	��Top=�6]   �� 5#   
   -   5        $ 	portLabel��5#      $
��Port��6#   $
��Left=�62   $��
371,80,0,0q�6M   $	��Top=�6a   G� 5$   
   -   5         G�Name$ passwordTextBox��5$      $
��Left=�6/   $��23��6J   $��200,142,0,0q�6V   $	��Top=�6k   $	��171��6�    G� PasswordChar$ *��6�   �� 5%   
   -   5        $ ipPortLabel��5%      $��IP adress :��6%   $
��Left=�6;   $��137,111,0,0q�6V   $	��Top=�6k   �� 5&   
   -	   5        $ passwordLabel��5&      $��
Password :��6'   $
��Left=�6<   $��135,142,0,0q�6W   $	��Top=�6l   �� 5'   
   -
   5        $ setHomeButton��5'      $
��Left=�6(   $��
10,207,0,0q�6C   $	��Top=�6W   $	��149��6o   $��65��6{   $��20(�6�   .��5(      �� .+�5)      �� -   5         ��Name$ SetHomeTextButton1��5)      $	��Set��68   $��Center=�6C   �� 5*      -   5        $ SetHomeTextButton2��5*      $
��Home��68   $��Center=�6D   5+      5,      �� 5-   
   -   5        $ connectHomeButton��5-      $
��Left=�6,   $��244,207,0,0q�6G   $	��Top=�6\   $	��149��6t   $��65��6�   $��20(�6�   .��5.      �� .+�5/      �� -   5        $ ConnectHomeTextButton1��5/      $��Connect��6<   $��Center=�6K   �� 50      -   5        $ ConnectHomeTextButton2��50      $
��Home��6<   $��Center=�6H   51      52      �� 53   
   -   5        $ connectLastIpButton��53      $
��Left=�6.   $��473,207,0,0q�6I   $	��Top=�6^   $	��149��6v   $��65��6�   $��20(�6�   .��54      �� .+�55      �� -   5        $ connectLastIpTextButton1��55      $��Connect��6>   $��Center=�6M   �� 56      -   5        $ connectLastIpTextButton2��56      $��Last Ip��6>   $��Center=�6M   57      58      �� 59   
   -   5        $ 
helpButton��59      #���   6%   $
��Left=�6V   $��
295,10,0,0q�6q   $	��Top=�6�   $��40��6�   $��40��6�   $��20(�6�   .��5:      �� $��12��6   $��20��5;      ��5<      ��  �� $ ButtonL�$ 
Foreground� 5=      !� 5>      $ Uniform=�6"     appbar_question# [ 5?   &   5A      5B      �� 5C   
   -   5        $ label��5C      $1��+*leave blank if the server is password free��6   $
��Left=�6U   $��376,142,0,0q�6p   $	��Top=�6�   $	��233��6�   5D      5E      