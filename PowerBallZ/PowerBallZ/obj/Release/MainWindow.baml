   M S B A M L   `   `   `  ����   
PowerBallZ    PowerBallZ.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml       � Icon$  Icon.png��$	��770��$	��555�� � Title$ 
PowerBallZ�� � 	Resources    TicketBorderColor �� Color  backgroundImage �� 	UriSource  foregroundImage �� 
TargetType  Win �� Property �� Value  FadeInBlocks �� TargetProperty	 W� From	
 W� To g� Duration��%&  &	        &	      &	 (     &	 =     &	 �     �� $ #ffcc66���� $ 	Wheel.png?��� $ 	Arrow.png?��� " ��.���  � ��$
 Bold"���  � ��$ 26(���  � ��$
 25 0q���  �@J�Opacity$ 0Q��� .��W� $ (TextBlock.Opacity)�$		 0.0X�$	
 1.0X�$ 0:0:5D�.��� -    �Name$ 
LayoutRoot��.+���  �� Orientation$ 
Horizontal=�.+�]� $��10q�.+�� $	��Top=�.+��� $��How many tickets?��$��5q��� -    ��Name$ NumberOfTickets��$��75��$��5q��� -   $��Generate Tickets��$��5q�
��������� 
���   �$��2q�$	��300��$��Bottom=�$
��Left=�._��  � ColumnDefinitions �� $��20 ��� $��* ��� $��20 � � RowDefinitions �� $��* �.+��� $��0��#��[  �� $��1�����#fff7e5.+��� $��
MEGA LOTTO��$��Segoe WP Black)�$��36(�$��Center=��� $��
50 20 50 0q��� $-��'Your chance to pick
this week's winner!��$��16(�$��Center=�$��5q��� $
��50 0q��� -    ��Name$ LotteryNumbers��
������ 
���   �$��0q�$��5q�$��Center=�$��
Levenim MT)�$��14(��� $��Barcode.png��$��50���� $��2��#��[  �� .+�� $
��10 0q�.+��� -    ��Name$ RotateImage��#��[ $	��400��$��10q�$
��Left=�$	��Top=� J� RenderTransformOrigin$ .5,.5&�z���  �� Angle$ 0Q�-   
Rotate��   Spin �� 
TargetName g� AutoReverse	 W� By��%W   &	       �� .��W� $ Rotate��$ Angle�$ 0:1:40D� .   �� *Angle+�� #��[ $	��140��$��0 80 0 0q�$	��Top=��� $��The Winning Numbers Are��$��30(�$��Center=�� -    �Name$ WinningNumberPanel��$��Center=�  �� BasedOn��%   '��       7  �� 8   " ��.+��� -    ��Name$ FirstNumber���� -	   $ SecondNumber���� -
   $ ThirdNumber���� -   $ FourthNumber���� -   $ FifthNumber���� -   $ PowerNumber��
���  ��