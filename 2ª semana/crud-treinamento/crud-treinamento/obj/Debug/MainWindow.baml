   M S B A M L   `   `   `  ����K  Gcrud-treinamento, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:crud_treinamentocrud_treinamento  5   	   !    crud_treinamento.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:crud_treinamento     5   	     � Title$  Cadastro de produtos��5   	   $	��450��6&   $	��800��63   .��5	      �  � RowDefinitions 5
   
   �� 5      $
��107* �6   �� 5      $
��110* �6   .+�5   
   �� -   5        $��Create��5   .   $
��Left=�6?   $��	78,86,0,0q�6Z   $	��Top=�6m   $��80��6�   ���	#FF57BB15��6   �� *
adiciona+6�   �� 5   
   -   5        $��
listaDados��5      $��2��6�   $��450,47,78,85q�6�   ��6%   �� *produtosFiltrados+ �� SelectedItem 6P   �� *produtoSelecionado+ �� View 5      �� 5      .P�5      ��  �� DisplayMemberBinding 6%   �� *Nome+.��6L   Nome5      �� 5       6%   �� *Preco+.��6M   Preço5      �� 5       6%   �� *Marca+.��6M   Marca5      �� 5       6%   �� *Tipo+.��6L   Tipo5      5      5      �� 5   
   -   5         ��Name$ update��5   ,   $��Update��6<   $
��Left=�6M   $��
174,86,0,0q�6h   $	��Top=�6|   ���	#FFCEC711$��80��6�   ��6   �� *update+6�   �� 5   
   -   5        $��Delete��5   ?   $
��Left=�6k   $��
270,86,0,0q�6�   $	��Top=�6�   $��80��6�   ���	#FFDE0D0D{�6   �� *produtosFiltrados.Count+��6P   �� *remove+6�   �� 5    
   -   5         �� SelectedValuePath$ Content��5       $
��Left=�6T   $��28��6o   $��
80,126,0,0q�6{   $	��Top=�6�   $	��117��6�    �� SelectedValue 6/   �� *
dadoFiltro+.@�5!      �� $��TODOS��6   �� 5"      $��Limpeza��6   �� 5#      $��Bebida��6   �� 5$      $��	Laticinio��6   5%      �� 5&   
   $��Filtrar��6-   $
��Left=�6?   $��260,127,0,0q�6Z   $	��Top=�6o   $��92��6�   ���	#FFFF8354$��27��6�   ��6   �� *	filtrar+6�   �� 5'   
   -   5        $ Content��5'      $
��Left=�6X   $��28��6s   $��
260,46,0,0q�6   $	��Top=�6�   $	��117��6�    6/   �� *dadoTipoCreate+.@�5(      �� $��Limpeza��6   �� 5)      $��Bebida��6   �� 5*      $��	Laticinio��6   5+      �� 5,   
   $.��(Selecione o tipo do produto para inserir��6   $
��Left=�6C   $��	27,47,0,0q�6^   $	��Top=�6q   5:      5;      