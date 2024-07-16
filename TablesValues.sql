
-------------TYPY ROŒLIN------

insert into PlantTypes values
('Warzywo'),
('Owoc'),
('Zio³o'),
('Kwiat')

--------GRUPY ROŒLIN------------

INSERT INTO PlantGroups VALUES
('Psianka',1),
('Dyniowate',1),
('Str¹czkowe',1),
('Kapustne',1),
('Liœciaste',1),
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
('Zewnêtrzne',4),
('Domowe',4);

---------SEKCJE ROŒLIN----------

INSERT INTO PlantSections VALUES
('Pomidor',1),
('Paparyka',1),
('Ziemniak',1),
('Bak³a¿an',1),
('Inne',1),
('Ogórek',2),
('Cukinia',2),
('Dynia',2),
('Patison',2),
('Inne',2),
('Fasolka',3),
('Groszek',3),
('Soczewica',3),
('Bób',3),
('Inne',3),
('Kapusta',4),
('Brukselka',4),
('Broku³',4),
('Kalafior',4),
('Kalarepa',4),
('Inne',4),
('Sa³ata',5),
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
('Wiœnia', 9),
('Brzoskiwnia', 9),
('Œliwka', 9),
('Morela', 9),
('Inne', 9),
('Truskawka', 10),
('Je¿yna', 10),
('Jagoda', 10),
('Malina', 10),
('Porzeczka', 10),
('Inne', 10),
('Jab³ko', 11),
('Gruszka', 11),
('Pigwa', 11),
('Granat ', 11),
('Inne', 11),
('Cytryna', 12),
('Mandarynka', 12),
('Pomarañcza', 12),
('Grejfrut', 12),
('Inne', 12),
('Banan', 13),
('Ananas', 13),
('Liczi', 13),
('Inne', 13);

------------KOLORY--------

insert into colors values
('Bia³y'),
('Czarny'),
('Czerwony'),
('Indigo'),
('Pomarañczowy'),
('Ró¿owy'),
('Wielokolorowy'),
('Zielony'),
('¯ó³ty')

------------PRZEZNACZENIE-------------

Insert into Destinations values
('Grunt'),
('Pod os³ony'),
('Doniczka');

------------WIELKOŒÆ OWOCU--------------

insert into FruitSizes values
('Ma³e',1,1,1),
('Typu Cherry',1,1,1),
('Wielkoowocowe',1,1,1),
('Srednioowocowe',1,1,1)

---------TYP OWOCU------------

Insert into FruitTypes values
('Miêsiste',1,1,1),
('Wielokomorowe',1,1,1),
('Ostre',1,1,2),
('S³odkie',1,1,2),
('Pod³u¿ne',1,1,2),
('Okr¹g³e',1,1,2),
('Cukrowy',1,3,2),
('£uskowy',1,3,2)

--------TYP WZROSTU-----------

Insert into GrowthTypes values
('Wysoko rosn¹cy',1,1,1),
('Kar³owy',1,1,1),
('Doniczkowy',1,1,1),
('Samokoñcz¹cy',1,1,1),
--('Krzak',1,1,2),
--('S³odka',1,1,2),
('Krzak',2,null,null),
('Drzewo',2,null,null),
('Roœlina pn¹ca',2,null,null),
('Roœlina zwisaj¹ca',2,null,null),
('Krzew',3,null,null),
('Winoroœl',3,null,null),
('Korzeñ',3,null,null),
('Kar³owa',1,3,1),
('Tyczna',1,3,1)


-----------WEGETACJA----------

Insert into GrowingSeazons values
('PóŸne'),
('Wczesne'),
('ŒredniopóŸne'),
('Œredniowczesne'),
('Jednoroczne'),
('Wieloletnie')

-----------WYSOKOŒÆ-------------

Insert into Heights values
('poni¿ej 0,7m'),
('0,7m - 1,2m'),
('0,8m - 1,3m'),
('1m - 1,8m'),
('1,2m - 1,6m'),
('1,5m - 2,5m'),
('powy¿ej 2m')

----------UMIEJSCOWIENIE--------

Insert into Positions values
('S³oneczne'),
('Pó³cieñ'),
('Cieñ')

------ZAPYLENIE----------

Insert into Pollinations values
('Obcopylne'),
('Samopylne'),
('Partenokarpiczne')

-------DODATKOWE CECHY---------

Insert into AdditionalFeatures values
('F1'),
('Mrozoodornoœæ'),
('Bezpieczne dla zwierz¹t'),
('Stare odmiany')



