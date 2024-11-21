
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
    note INT null,
    primary key (id),
    foreign key (nom_activite) references Activites(nom)
);

INSERT INTO Seances (nom_activite, date, heure, nbr_places, note) values ('Canot-Camping', '2024-11-21', '14:00:00' , 20, null);
INSERT INTO Seances (nom_activite, date, heure, nbr_places, note) values ('Escalade', '2024-12-04', '13:20:00' , 20, null);
INSERT INTO Seances (nom_activite, date, heure, nbr_places, note) values ('Canot-Camping', '2024-08-21', '13:30:00' , 20, null);
INSERT INTO Seances (nom_activite, date, heure, nbr_places, note) values ('Peintures', '2024-10-15', '12:20:00' , 20, null);
INSERT INTO Seances (nom_activite, date, heure, nbr_places, note) values ('Volleyball', '2024-01-17', '12:10:00' , 20, null);
INSERT INTO Seances (nom_activite, date, heure, nbr_places, note) values ('Tyrolienne', '2024-01-20', '11:20:00' , 20, null);
INSERT INTO Seances (nom_activite, date, heure, nbr_places, note) values ('Via ferrata', '2024-02-02', '12:20:00' , 20, null);
INSERT INTO Seances (nom_activite, date, heure, nbr_places, note) values ('Canot-Camping', '2024-11-22', '13:15:00' , 20, null);
INSERT INTO Seances (nom_activite, date, heure, nbr_places, note) values ('Dentifrice d`éléphants', '2024-11-24', '12:25:00' , 20, null);
INSERT INTO Seances (nom_activite, date, heure, nbr_places, note) values ('Canot-Camping', '2024-04-30', '10:40:00' , 20, null);
INSERT INTO Seances (nom_activite, date, heure, nbr_places, note) values ('Équitation', '2024-11-30', '11:45:00' , 20, null);
INSERT INTO Seances (nom_activite, date, heure, nbr_places, note) values ('Équitation', '2024-07-23', '13:10:00' , 20, null);
INSERT INTO Seances (nom_activite, date, heure, nbr_places, note) values ('Cirque', '2024-11-21', '10:20:00' , 20, null);
INSERT INTO Seances (nom_activite, date, heure, nbr_places, note) values ('Club de lecture', '2024-11-21', '09:20:00' , 20, null);




DROP TABLE if exists Inscriptions;
CREATE TABLE Inscriptions (
    id_adherent VARCHAR(11),
    id_seance INT,
    PRIMARY KEY (id_adherent, id_seance),
    foreign key (id_adherent) references Adherents(no_identification),
    FOREIGN KEY (id_seance) references Seances(id)
);

INSERT INTO Inscriptions (id_adherent, id_seance) values ('AA-2005-623', 1);







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

DELIMITER //
CREATE TRIGGER gerer_nbrPlaces_seances
    AFTER INSERT ON Inscriptions
    FOR EACH ROW
    BEGIN
        DECLARE nbrPlaces INT ;
        SET nbrPlaces = (SELECT nbr_places FROM Seances where id = NEW.id_seance) - 1;

        UPDATE Seances SET nbr_places = nbrPlaces WHERE id = NEW.id_seance;


    end //
DELIMITER ;


DELIMITER //
CREATE TRIGGER verifier_nombrePlaces_seances
    BEFORE INSERT ON Inscriptions
    FOR EACH ROW
    BEGIN
        IF (SELECT nbr_places FROM Seances WHERE id = NEW.id_seance) = 0 THEN
            SIGNAL SQLSTATE '45000'
                SET MESSAGE_TEXT = 'Un Participant ne peut pas être ajouté. Le nombre de places restantes est de 0.';


        end if ;
    end //
DELIMITER ;


/* Création des procédures */
DELIMITER //
CREATE PROCEDURE insertion_inscriptions (IN idDeAdherent VARCHAR(11), IN idDeSeances INT)
BEGIN
    INSERT INTO Inscriptions (id_adherent, id_seance) VALUES (idDeAdherent, idDeSeances);
end //
DELIMITER ;



/* Création des fonctions */

/* !!!!! pas fini !!!!!*/
DELIMITER //
CREATE FUNCTION NbrTotal_Adherents (nomActivite VARCHAR(11)) RETURNS INT
BEGIN
    SELECT COUNT(I.id_adherent)
    FROM Activites
    INNER JOIN Seances S on Activites.nom = S.nom_activite
    INNER JOIN Inscriptions I on S.id = I.id_seance
    GROUP BY S.id;
end //
DELIMITER ;



