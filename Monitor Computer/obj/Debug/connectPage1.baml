   M S B A M L   `   `   `  ����T  PMahApps.Metro, Version=1.2.4.0, Culture=neutral, PublicKeyToken=f4fb5a3c4d1e5b4fV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls  5      J FMonitorComputer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:Monitor_ComputerMonitor_Computer 5      #   Monitor_Computer.connectPage1   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      PControls;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      0localclr-namespace:Monitor_Computer     5        N� Title$  connectPage1��5
      .��5      � .+�5   
   �� -   5         ��Name$ connectButton��5      $��Connect��6(   $
��Left=�6:   $��457,203,0,0q�6U   $	��Top=�6j   $	��165��6�   $��69��6�   $��20(�6�   $��Segoe UI Black)�6�   6�   �� 5   
   -   5        $ serverButton��5      $��Server��6'   $
��Left=�68   $��
457,10,0,0q�6S   $	��Top=�6g   $	��165��6   $��69��6�   $��20(�6�   $��Segoe UI Black)�6�   6�   �� 5   
   -   5         ��Name$ ipAdressLabel��5      $-��'Public ip adress : trying to find it...��6'   $
��Left=�6Y   $��	10,10,0,0q�6t   $	��Top=�6�   �� 5   
   -   5        $ localIpAdresLabel��5      $��Local ip adress : ��6+   $
��Left=�6H   $��	10,35,0,0q�6c   $	��Top=�6v   �� 5   
   -   5        $ serverPortLabel��5      $��Server port : -��6)   $
��Left=�6C   $��	10,61,0,0q�6^   $	��Top=�6q   �� 5   
   -   5        $ 	setFolder��5      $��
Set Folder��6$   $
��Left=�69   $��
10,203,0,0q�6T   $	��Top=�6h   $	��165��6�   $��69��6�   $��20(�6�   $��Segoe UI Black)�6�   6�   �� 5   
   -   5        $ stopBooting��5      $
��Left=�6&   $��457,105,0,0q�6A   $	��Top=�6V   $	��165��6n   $��69��6z   $��20(�6�   $��Segoe UI Black)�6�   .��5      �� .+�5      �� -   5         ��Name$ ConnectHomeTextButton1��5      $��STOP STARTUP��6<   $��Center=�6P   �� 5      -	   5        $ ConnectHomeTextButton2��5      $��BOOTING��6<   $��Center=�6K   5      5      �� 5   
   -
   5        $ helpButtonConnect1��5         MetroCircleButtonStyle#���   6-   $
��Left=�6^   $��
295,10,0,0q�6y   $	��Top=�6�   $��40��6�   $��40��6�   $��20(�6�   .��5      �� $��12��6   $��20��5      ��5      �� *
Foreground+ �� RelativeSource ��  �� AncestorType" �� J� OpacityMask 5      !� 5       j� Stretch$ Uniform=�6"     appbar_question !� Visual# [ 5      5       5!      �� 5"   
   -   5        $ 
chatButton��5"      $
��Chat��6%   $
��Left=�64   $��233,203,0,0q�6O   $	��Top=�6d   $	��165��6|   $��69��6�   $��20(�6�   $��Segoe UI Black)�6�   6�   5#      5$      