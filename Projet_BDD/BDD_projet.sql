
DROP TABLE if exists Administrateur;
create table Administrateur (
	id INT,
	mdp VARCHAR(50),
	PRIMARY KEY (id)
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
	age INT,
	PRIMARY KEY (no_identification)
);


insert into Adherents ( nom, prenom, adresse, date_naissance) values ( 'Messier', 'Manfred', '03 Homewood Place', '2005-02-08' );
insert into Adherents ( nom, prenom, adresse, date_naissance) values ( 'Martensen', 'Tamqrah', '63587 Fairview Avenue', '2006-06-23' );
insert into Adherents ( nom, prenom, adresse, date_naissance) values ( 'Mendoza', 'Annamarie', '3758 Texas Street', '2006-05-08');
insert into Adherents ( nom, prenom, adresse, date_naissance) values ( 'Onions', 'Homere', '0 Hermina Avenue', '2006-09-08');
insert into Adherents ( nom, prenom, adresse, date_naissance) values ( 'Applewhite', 'Dierdre', '4875 Lerdahl Center', '2005-06-19');
insert into Adherents ( nom, prenom, adresse, date_naissance) values ( 'Muscroft', 'Coleman', '350 Esch Street', '2006-01-27');
insert into Adherents ( nom, prenom, adresse, date_naissance) values ( 'Coupar', 'Cello', '49627 Arapahoe Parkway', '2005-02-13');
insert into Adherents ( nom, prenom, adresse, date_naissance) values ( 'Astill', 'Abigael', '06 Esker Plaza', '2005-03-13');
insert into Adherents ( nom, prenom, adresse, date_naissance) values ('Welsh', 'Siegfried', '1 Hanover Court', '2006-06-03');
insert into Adherents ( nom, prenom, adresse, date_naissance) values ( 'Choak', 'Bette-ann', '82244 Johnson Lane', '2005-06-05');



CREATE TABLE Categories (
    id INT AUTO_INCREMENT,
    nom VARCHAR(50),
    PRIMARY KEY (id)
);

INSERT INTO Categories (nom) VALUES ('Sports');
INSERT INTO Categories (nom) VALUES ('Sciences');
INSERT INTO Categories (nom) VALUES ('Arts');
INSERT INTO Categories (nom) VALUES ('Divertissements');
INSERT INTO Categories (nom) VALUES ('Lectures');


DROP TABLE if exists Activites;
create table Activites (
	nom VARCHAR(50),
	id_categorie INT,
	type VARCHAR(50),
	cout_organisation VARCHAR(50),
	prix_vente VARCHAR(50),
	primary key (nom),
	foreign key (id_categorie) references categories (id)
);
insert into Activites (nom, id_categorie, type, cout_organisation, prix_vente) values ('Canot-Camping', 1, 'Plein-air', '$228.90', '$138.09');
insert into Activites (nom, id_categorie, type, cout_organisation, prix_vente) values ('Escalade', 1, 'Ludo-sportive', '$169.02', '$189.26');
insert into Activites (nom, id_categorie, type, cout_organisation, prix_vente) values ('Peintures', 3, 'Peintures', '$288.13', '$209.42');
insert into Activites (nom, id_categorie, type, cout_organisation, prix_vente) values ('Volleyball', 1, 'Activité d`intérieur', '$202.60', '$192.33');
insert into Activites (nom, id_categorie, type, cout_organisation, prix_vente) values ('Cirque', 4, 'Spectacle', '$297.79', '$220.15');
insert into Activites (nom, id_categorie, type, cout_organisation, prix_vente) values ('Équitation', 1, 'Plein air', '$235.72', '$214.00');
insert into Activites (nom, id_categorie, type, cout_organisation, prix_vente) values ('Club de lecture', 5, 'Lecture', '$153.43', '$159.17');
insert into Activites (nom, id_categorie, type, cout_organisation, prix_vente) values ('Tyrolienne', 4, 'Plein air', '$178.32', '$292.27');
insert into Activites (nom, id_categorie, type, cout_organisation, prix_vente) values ('Via ferrata', 1, 'Plein air', '$264.24', '$275.55');
insert into Activites (nom, id_categorie, type, cout_organisation, prix_vente) values ('Dentifrice d`éléphants', 2, 'Activité d`intérieur', '$178.06', '$127.03');




DROP TABLE if exists Seances;
CREATE TABLE Seances (
    id INT AUTO_INCREMENT,
    nom_activite VARCHAR(50),
    date DATE ,
    heure TIME,
    nbr_places INT,
    note INT,
    primary key (id),
    foreign key (nom_activite) references Activites(nom)
);



DROP TABLE if exists Inscriptions;
CREATE TABLE Inscriptions (
    id_adherent VARCHAR(11),
    id_seance INT,
    PRIMARY KEY (id_adherent, id_seance),
    foreign key (id_adherent) references Adherents(no_identification),
    FOREIGN KEY (id_seance) references Seances(id)
);





/* Création des triggers */
DELIMITER //
CREATE TRIGGER calculer_age
    BEFORE INSERT ON Adherents
    FOR EACH ROW
    BEGIN
        DECLARE ageCalcule INT;
        SET ageCalcule = DATEDIFF(NEW.date_naissance, CURRENT_DATE());

        SET NEW.age = ageCalcule;

    end //
DELIMITER ;



DELIMITER //
CREATE TRIGGER creer_numeroIdentification
    BEFORE INSERT ON Adherents
    FOR EACH ROW
    BEGIN
        SET NEW.no_identification = CONCAT(SUBSTRING(NEW.prenom, 1, 1), SUBSTRING(NEW.nom, 1, 1), '-', YEAR(NEW.date_naissance), '-', FLOOR(1+(RAND()*(9 - 1 + 1))),  FLOOR(1+(RAND()*(9 - 1 + 1))),  FLOOR(1+(RAND()*(9 - 1 + 1)))   );
    end //
DELIMITER ;


