   M S B A M L   `   `   `  ����  Desktop_Clock    DesktopClock.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  Desktop Clock��5   	   $	��200��6   $	��400��6,    � WindowStyle$
 None=�5   	    � AllowsTransparency$
 True��5   	   
������  � ShowInTaskbar$ False��5   	    � WindowStartupLocation$ CenterScreen=�5	   	    � 
ResizeMode$ NoResize=�5
   	   .��5      ��  �� CornerRadius$ 20��6    �� Padding$ 0q�6   $��10q�6+   $
}�True��67   ._�5   
   � .+�5      �� $��Center=�6   $��Center=�66   .+�5      �� -   5        $��LocationTempTextBlock��5      $��16(�69   
�������$
��Bold"�6Z    �� TextAlignment$ Center=�6l   $��0,0,0,10q�6�   �� 5      	 �� Orientation$	 
Horizontal=�6   $��Center=�66   .+�5      �� $ 10��6   ���	#882E2E2E$	 8,3q�6F   $��0,0,6,0q�6T      DropShadowEffect
 J� Effect#
 �   6e   ._�5      �� -   5        $��HourTextBlock��5   $   $��11��69   $��36(�6C   $
��Bold"�6Q   
�������$��Center=�6v   $��Center=�6�   5      �� 5      $��:��6    $��36(�6)   
�������$��Center=�6J   $��0,0,6,0q�6e   �� 5      $ 10��6   ���	#882E2E2E$	 8,3q�6F   $��0,0,6,0q�6T   #
 �   6e   ._�5      � .+�5      �� -   5        $��MinuteTextBlock��5   (   $��20��6?   $��36(�6I   $
��Bold"�6W   
�������$��Center=�6|   $��Center=�6�   5      5      �� 5       $��:��6    $��36(�6)   
�������$��Center=�6J   $��0,0,6,0q�6e   �� 5"      $ 10��6   ���	#882E2E2E$	 8,3q�6F   $��0,0,6,0q�6T   #
 �   6e   ._�5#      �� -   5        $��SecondTextBlock��5#   $   $��39��6;   $��36(�6E   $
��Bold"�6S   
�������$��Center=�6x   $��Center=�6�   5$      �� 5&      $	 Vertical=�6!   $	��Top=�68   $��0q�6P   .+�5'      �� $��PM��6$   $��14(�6.   $
��Bold"�6<   
�������$
��Left=�6a   5(      5)      �� 5+      -   5        $��DateTextBlock��5+      $��18(�61   
�������$ Center=�6R   $��0,10,0,0q�6i   $
��Bold"�6{   $��Italic$�6�   5,      5-      5.       � 	Resources 50      3  -System.Windows.Media.Effects.DropShadowEffect   Color   
BlurRadius   ShadowDepth��%H   &	          51   
   $ 	#80000000��64   $ 10Q�6F   $ 2Q�6V   52      53      