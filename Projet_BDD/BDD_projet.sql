
DROP TABLE if exists Administrateur;
create table Administrateur (
	id INT,
	mdp VARCHAR(50)
);
insert into Administrateur (id, mdp) values (1, 'sE898627uZ');
insert into Administrateur (id, mdp) values (2, 'bF12521');
insert into Administrateur (id, mdp) values (3, 'oR77933');
insert into Administrateur (id, mdp) values (4, 'lM29601');
insert into Administrateur (id, mdp) values (5, 'xT98100UJ');
insert into Administrateur (id, mdp) values (6, 'vG29184');
insert into Administrateur (id, mdp) values (7, 'qR52474');
insert into Administrateur (id, mdp) values (8, 'dB82056E|s');
insert into Administrateur (id, mdp) values (9, 'iC67973v|');
insert into Administrateur (id, mdp) values (10, 'cR743226Y');



DROP TABLE if exists Adherents;
create table Adherents (
	no_identification VARCHAR(11),
	nom VARCHAR(50),
	prenom VARCHAR(50),
	adresse VARCHAR(50),
	date_naissance DATE,
	age INT
);
insert into Adherents ( nom, prenom, adresse, date_naissance, age) values ( 'Messier', 'Manfred', '03 Homewood Place', '2005-02-08', 25);
insert into Adherents ( nom, prenom, adresse, date_naissance, age) values ( 'Martensen', 'Tamqrah', '63587 Fairview Avenue', '2006-06-23', 27);
insert into Adherents ( nom, prenom, adresse, date_naissance, age) values ( 'Mendoza', 'Annamarie', '3758 Texas Street', '2006-05-08', 21);
insert into Adherents ( nom, prenom, adresse, date_naissance, age) values ( 'Onions', 'Homere', '0 Hermina Avenue', '2006-09-08', 30);
insert into Adherents ( nom, prenom, adresse, date_naissance, age) values ( 'Applewhite', 'Dierdre', '4875 Lerdahl Center', '2005-06-19', 19);
insert into Adherents ( nom, prenom, adresse, date_naissance, age) values ( 'Muscroft', 'Coleman', '350 Esch Street', '2006-01-27', 27);
insert into Adherents ( nom, prenom, adresse, date_naissance, age) values ( 'Coupar', 'Cello', '49627 Arapahoe Parkway', '2005-02-13', 19);
insert into Adherents ( nom, prenom, adresse, date_naissance, age) values ( 'Astill', 'Abigael', '06 Esker Plaza', '2005-03-13', 22);
insert into Adherents ( nom, prenom, adresse, date_naissance, age) values ('Welsh', 'Siegfried', '1 Hanover Court', '2006-06-03', 23);
insert into Adherents ( nom, prenom, adresse, date_naissance, age) values ( 'Choak', 'Bette-ann', '82244 Johnson Lane', '2005-06-05', 22);



DROP TABLE if exists Inscriptions;
CREATE TABLE Inscriptions (
    id_adherent VARCHAR(11),
    id_seance INT
);
insert into Inscriptions (id_seance) values ( 1);
insert into Inscriptions (id_seance) values ( 2);
insert into Inscriptions (id_seance) values ( 3);
insert into Inscriptions (id_seance) values ( 4);
insert into Inscriptions (id_seance) values ( 5);
insert into Inscriptions (id_seance) values ( 6);
insert into Inscriptions (id_seance) values ( 7);
insert into Inscriptions (id_seance) values ( 8);
insert into Inscriptions (id_seance) values ( 10);


CREATE TABLE Categories (
    id INT AUTO_INCREMENT,
    nom VARCHAR(50)
);

INSERT INTO Categories (nom) VALUES ('Sports');
INSERT INTO Categories (nom) VALUES ('Sciences');
INSERT INTO Categories (nom) VALUES ('Arts');
INSERT INTO Categories (nom) VALUES ('Divertissements');
INSERT INTO Categories (nom) VALUES ('Lectures');


create table Activites (
	nom VARCHAR(50),
	id_categorie INT,
	type VARCHAR(50),
	cout_organisation VARCHAR(50),
	prix_vente VARCHAR(50)
);
insert into Activites (nom, id_categorie, type, cout_organisation, prix_vente) values ('Canot-Camping', 1, 'Transcof', '$228.90', '$138.09');
insert into Activites (nom, id_categorie, type, cout_organisation, prix_vente) values ('Escalade', 2, 'Vagram', '$169.02', '$189.26');
insert into Activites (nom, id_categorie, type, cout_organisation, prix_vente) values ('Peintures', 3, 'Greenlam', '$288.13', '$209.42');
insert into Activites (nom, id_categorie, type, cout_organisation, prix_vente) values ('Volleyball', 4, 'Flowdesk', '$202.60', '$192.33');
insert into Activites (nom, id_categorie, type, cout_organisation, prix_vente) values ('Cirque', 5, 'Stringtough', '$297.79', '$220.15');
insert into Activites (nom, id_categorie, type, cout_organisation, prix_vente) values ('Équitation', 6, 'Aerified', '$235.72', '$214.00');
insert into Activites (nom, id_categorie, type, cout_organisation, prix_vente) values ('Club de lecture', 7, 'Holdlamis', '$153.43', '$159.17');
insert into Activites (nom, id_categorie, type, cout_organisation, prix_vente) values ('Tyrolienne', 8, 'Viva', '$178.32', '$292.27');
insert into Activites (nom, id_categorie, type, cout_organisation, prix_vente) values ('Via ferrata', 9, 'Sonsing', '$264.24', '$275.55');
insert into Activites (nom, id_categorie, type, cout_organisation, prix_vente) values ('Dissection de coeur de boeuf', 10, 'Gembucket', '$178.06', '$127.03');

DROP TABLE if exists Inscriptions;
CREATE TABLE Inscriptions (
    id INT AUTO_INCREMENT,
    nom VARCHAR(50)
);