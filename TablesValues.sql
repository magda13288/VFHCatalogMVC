
-------------TYPY RO�LIN------

insert into PlantTypes values
('Warzywo'),
('Owoc'),
('Zio�o'),
('Kwiat')

--------GRUPY RO�LIN------------

INSERT INTO PlantGroups VALUES
('Psianka',1),
('Dyniowate',1),
('Str�czkowe',1),
('Kapustne',1),
('Li�ciaste',1),
('Cebulowe',1),
('Korzeniowe',1),
('Rzepowate',1),
('Pestkowe',2),
('Jagodowe',2),
('Ziarnkowe',2),
('Cytrusowe',2),
('Egzotyczne',2),
('Lecznicze',3),
('Przyprawowe',3),
('Olejkodajne',3),
('Zewn�trzne',4),
('Domowe',4);

---------SEKCJE RO�LIN----------

INSERT INTO PlantSections VALUES
('Pomidor',1),
('Paparyka',1),
('Ziemniak',1),
('Bak�a�an',1),
('Inne',1),
('Og�rek',2),
('Cukinia',2),
('Dynia',2),
('Patison',2),
('Inne',2),
('Fasolka',3),
('Groszek',3),
('Soczewica',3),
('B�b',3),
('Inne',3),
('Kapusta',4),
('Brukselka',4),
('Broku�',4),
('Kalafior',4),
('Kalarepa',4),
('Inne',4),
('Sa�ata',5),
('Szpinak',5),
('Natka pietruszki',5),
('Inne',5),
('Cebula',6),
('Czosnek',6),
('Por',6),
('Inne',6),
('Marchewka',7),
('Pietruszka',7),
('Burak',7),
('Seler', 7),
('Inne', 7),
('Rzodkiewka', 8),
('Brukiew', 8),
('Rzepa', 8),
('Inne', 8),
('Wi�nia', 9),
('Brzoskiwnia', 9),
('�liwka', 9),
('Morela', 9),
('Inne', 9),
('Truskawka', 10),
('Je�yna', 10),
('Jagoda', 10),
('Malina', 10),
('Porzeczka', 10),
('Inne', 10),
('Jab�ko', 11),
('Gruszka', 11),
('Pigwa', 11),
('Granat ', 11),
('Inne', 11),
('Cytryna', 12),
('Mandarynka', 12),
('Pomara�cza', 12),
('Grejfrut', 12),
('Inne', 12),
('Banan', 13),
('Ananas', 13),
('Liczi', 13),
('Inne', 13);

------------KOLORY--------

insert into colors values
('Bia�y'),
('Czarny'),
('Czerwony'),
('Indigo'),
('Pomara�czowy'),
('R�owy'),
('Wielokolorowy'),
('Zielony'),
('��ty')

------------PRZEZNACZENIE-------------

Insert into Destinations values
('Grunt'),
('Pod os�ony'),
('Doniczka');

------------WIELKO�� OWOCU--------------

insert into FruitSizes values
('Ma�e',1,1,1),
('Typu Cherry',1,1,1),
('Wielkoowocowe',1,1,1),
('Srednioowocowe',1,1,1)

---------TYP OWOCU------------

Insert into FruitTypes values
('Mi�siste',1,1,1),
('Wielokomorowe',1,1,1),
('Ostre',1,1,2),
('S�odkie',1,1,2),
('Pod�u�ne',1,1,2),
('Okr�g�e',1,1,2),
('Cukrowy',1,3,2),
('�uskowy',1,3,2)

--------TYP WZROSTU-----------

Insert into GrowthTypes values
('Wysoko rosn�cy',1,1,1),
('Kar�owy',1,1,1),
('Doniczkowy',1,1,1),
('Samoko�cz�cy',1,1,1),
--('Krzak',1,1,2),
--('S�odka',1,1,2),
('Krzak',2,null,null),
('Drzewo',2,null,null),
('Ro�lina pn�ca',2,null,null),
('Ro�lina zwisaj�ca',2,null,null),
('Krzew',3,null,null),
('Winoro�l',3,null,null),
('Korze�',3,null,null),
('Kar�owa',1,3,1),
('Tyczna',1,3,1)


-----------WEGETACJA----------

Insert into GrowingSeazons values
('P�ne'),
('Wczesne'),
('�redniop�ne'),
('�redniowczesne'),
('Jednoroczne'),
('Wieloletnie')

-----------WYSOKO��-------------

Insert into Heights values
('poni�ej 0,7m'),
('0,7m - 1,2m'),
('0,8m - 1,3m'),
('1m - 1,8m'),
('1,2m - 1,6m'),
('1,5m - 2,5m'),
('powy�ej 2m')

----------UMIEJSCOWIENIE--------

Insert into Positions values
('S�oneczne'),
('P�cie�'),
('Cie�')

------ZAPYLENIE----------

Insert into Pollinations values
('Obcopylne'),
('Samopylne'),
('Partenokarpiczne')

-------DODATKOWE CECHY---------

Insert into AdditionalFeatures values
('F1'),
('Mrozoodorno��'),
('Bezpieczne dla zwierz�t'),
('Stare odmiany')



