   M S B A M L   `   `   `  ����  CarmelTest2    CarmelTest.MainWindow   5   	   \ XPresentationFramework, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   8x,http://schemas.microsoft.com/winfx/2006/xaml    5   	     � Title$  
MainWindow��5   	   $	��500��6   $	��750��6)   .��5      � 
�������.+�5   
   �� -   5         ��Name$
 tabs��5       �� SelectedIndex$ 0��6#   
������� �� TabStripPlacement$
 Left=�6L   $
��Left=�6e   $	��470��6�   $��
-52,0,0,-1q�6�   $	��Top=�6�   $	��794��6�   .@�5      �� $y�Hidden=�6   $��TabItem��6*   .��5      � .+�5	      �� -   5         ��Name$ Import��5	      $��Import
��6,   $
��Left=�6B   $��
243,39,0,0q�6]   $	��Top=�6q   $��88��6�   $��31��6�   �� 5
      -   5        $ Import_Copy��5
      $��
Template

��61   $
��Left=�6N   $��
243,75,0,0q�6i   $	��Top=�6}   $��88��6�   $��31��6�   �� 5      -   5        $ Import_Copy1��5      $��Nieuwe locatie:
��62   $
��Left=�6Q   $��243,111,0,0q�6l   $	��Top=�6�   $	��100��6�   $��31��6�   �� 5      -   5        $ Import_Copy2��5      $��Oude locatie:
��62   $
��Left=�6O   $��243,147,0,0q�6j   $	��Top=�6   $��88��6�   $��31��6�   �� 5      -   5         ��Name$ importTextbox��5   "   $
��Left=�69   $��31��6T   $��
359,39,0,0q�6`   $	��Top=�6t   $	��320��6�   . �5      /� .T�5      J� .*�6(   .*�5   "   �� $(�"Klik om een rooster te importeren.��6&   5      5      5      �� 5      $
��Left=�6"   $��31��6=   $��
359,75,0,0q�6I   $	��Top=�6]   $	��320��6u   . �5      /� .T�5      J� .*�6(   .*�5   "   �� $�50 minuten rooster��6&   5      5      5      �� 5      -   5        $ 
padTextbox��5   "   $
��Left=�66   $��31��6Q   $��359,111,0,0q�6]   $	��Top=�6r   $	��320��6�   . �5      /� .T�5      J� .*�6(   .*�5   "   �� $)�#Klik om een file path te selecteren��6&   5      5       5!      �� 5"      $
��Left=�6"   $��31��6=   $��359,147,0,0q�6I   $	��Top=�6^   $	��320��6v   . �5#      /� .T�5$      J� .*�6(   .*�5%   "   �� $�C://oud��6&   5&      5'      5(      �� 5)      -   5         ��Name$ converteerButton��5)      $��
Converteer��67   $
��Left=�6L   $��560,211.701,0,0q�6g   $	��Top=�6�    J� RenderTransformOrigin$ 
5.547,2.85&�6�   |�.  6�   5,      5-      �� 5.      $y�Hidden=�6   $��TabItem��6*   .��5/      � .+�50      �� $��	Templates��6      headerLabel#��[  61   $ -0.666,-1.149&�6V   $ ��180.275,10,447.808,416.337q�6|   51      52      �� 54      $y�Hidden=�6   $��TabItem��6*   .��55      � .+�56      �� $��Vleugels��6   #��[  60   $ -0.666,-1.149&�6U   $ ��180.275,10,447.808,416.337q�6{   57      58      �� 5:      $y�Hidden=�6   $��TabItem��6*   .��5;      � .+�5<      �� $��Accounts��6   #��[  60   $ -0.666,-1.149&�6U   $ ��180.275,10,447.808,416.337q�6{   5=      5>      �� 5@      $y�Hidden=�6   $��TabItem��6*   .��5A      � .+�5B      �� $��Settings��6   #��[  6/   $ -0.666,-1.149&�6T   $ ��180.275,10,447.808,416.337q�6z   5C      5D      5F      �� 5G   
   $
��Left=�6   $	��469��6,   $	��Top=�69   $	��177��6Q   ���	#FF832083.+�5H      �� $��87��6    �� Left$ 24.358��6    
	 �� Top$	 310.448��65   $	��130��6J   $ 0.478,0.522&�6V   $��images/CarmelLogo.jpg��6z   5I      �� 5J   
   -	   5        $ templatesButton��5J        
menuButton#��[ 6*   $��	Templates��6O   $
��Left=�6c   $��	32,64,0,0q�6~   $	��Top=�6�   $ 
5.547,2.85&�6�   6�   �� 5K   
   -
   5        $ vleugelButton��5K      #��[ 6(   $��Vleugels��6L   $
��Left=�6_   $��
32,109,0,0q�6z   $	��Top=�6�   $ 
5.547,2.85&�6�   6�   �� 5L   
   -   5        $ accountsButton��5L      #��[ 6)   $��Accounts��6M   $
��Left=�6`   $��
32,154,0,0q�6{   $	��Top=�6�   $ 
5.547,2.85&�6�   6�   �� 5M   
   -   5        $ settingsButton��5M      #��[ 6)   $��Settings��6M   $
��Left=�6`   $��
32,410,0,0q�6{   $	��Top=�6�   $ 
5.547,2.85&�6�   6�   �� 5N   
   -   5        $ convertButton��5N      #��[ 6(   $��Convert��6L   $
��Left=�6^   $��	32,19,0,0q�6y   $	��Top=�6�   $ 
5.547,2.85&�6�   6�   5O      5P      