DROP TABLE if exists Evaluations;
DROP TABLE if exists Inscriptions;
DROP TABLE if exists Seances;
DROP TABLE if exists Activites;
DROP TABLE if exists Categories;
DROP TABLE if exists Administrateur;
DROP TABLE if exists Adherents;





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




create table Adherents (
	no_identification VARCHAR(11),
	nom VARCHAR(50),
	prenom VARCHAR(50),
	adresse VARCHAR(50),
	date_naissance DATE,
	age INT,
	PRIMARY KEY (no_identification)
);



CREATE TABLE Categories (
    id INT AUTO_INCREMENT,
    nom VARCHAR(50),
    PRIMARY KEY (id)
);



create table Activites (
	nom VARCHAR(50),
	id_categorie INT,
	type VARCHAR(50),
	cout_organisation DOUBLE,
	prix_vente DOUBLE,
	primary key (nom),
	foreign key (id_categorie) references categories (id)
);



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



CREATE TABLE Inscriptions (
    id_adherent VARCHAR(11),
    id_seance INT,
    PRIMARY KEY (id_adherent, id_seance),
    foreign key (id_adherent) references Adherents(no_identification),
    FOREIGN KEY (id_seance) references Seances(id)
);



CREATE TABLE Evaluations (
    id_adherent VARCHAR(11),
    id_seance INT,
    note INT,
    PRIMARY KEY (id_adherent, id_seance),
    foreign key (id_adherent) references Adherents(no_identification),
    FOREIGN KEY (id_seance) references Seances(id)
);



/* Création des triggers */
DROP TRIGGER if exists calculer_age;
DELIMITER //
CREATE TRIGGER calculer_age
    BEFORE INSERT ON Adherents
    FOR EACH ROW
    BEGIN
        DECLARE ageCalcule INT;
        SET ageCalcule = FLOOR((DATEDIFF(CURRENT_DATE(), NEW.date_naissance ))/365);

        IF ageCalcule >= 18 THEN
            SET NEW.age = ageCalcule;

        ELSE SIGNAL SQLSTATE '45000'
            SET MESSAGE_TEXT = 'Un Participant ne peut pas être ajouté. Son age doit être de plus de 18.';

        end if ;
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

DROP TRIGGER if exists verifier_evaluations;
DELIMITER //
CREATE TRIGGER verifier_evaluations
    BEFORE INSERT ON Evaluations
    FOR EACH ROW
    BEGIN


        DECLARE idSeanceEval INT;
        DECLARE idAdherentsEval VARCHAR(11);
        SET idSeanceEval = NEW.id_seance;
        SET idAdherentsEval = NEW.id_adherent;



        IF (SELECT COUNT(*) FROM Inscriptions WHERE Inscriptions.id_seance = idSeanceEval AND Inscriptions.id_adherent = idAdherentsEval ) <= 0 THEN
            SIGNAL SQLSTATE '45000'
                SET MESSAGE_TEXT = 'Un Participant ne peut pas donner de notes à une séance s`il n`est pas inscrits à celle-ci';
        end if ;

    end //
DELIMITER ;


DROP TRIGGER if exists moyenneNotes_seances;
DELIMITER //
CREATE TRIGGER moyenneNotes_seances
    AFTER INSERT ON Evaluations
    FOR EACH ROW
    BEGIN

        DECLARE nouvIdSeance INT ;
        SET nouvIdSeance = NEW.id_seance;

        UPDATE Seances SET note = (SELECT AVG(note)
                                   FROM Evaluations
                                   WHERE id_seance = nouvIdSeance) WHERE id = nouvIdSeance;


    end //
DELIMITER ;




/* Création des procédures */

DROP PROCEDURE if exists insertion_inscriptions;
DELIMITER //
CREATE PROCEDURE insertion_inscriptions (IN idDeAdherent VARCHAR(11), IN idDeSeances INT)
BEGIN
    INSERT INTO Inscriptions (id_adherent, id_seance) VALUES (idDeAdherent, idDeSeances);
end //
DELIMITER ;




DROP PROCEDURE if exists insertion_evaluations;
DELIMITER //
CREATE PROCEDURE insertion_evaluations (IN idDeAdherent VARCHAR(11), IN idDeSeances INT, IN noteDeSeances INT)
BEGIN
    INSERT INTO Evaluations (id_adherent, id_seance, note) VALUES (idDeAdherent, idDeSeances, noteDeSeances);
end //
DELIMITER ;


DROP PROCEDURE if exists insertion_seances;
DELIMITER //
CREATE PROCEDURE insertion_seances ( IN seance_nomActivite VARCHAR(50), IN seance_date DATE, IN seance_heure TIME, IN seance_nbrPlace INT )
BEGIN
    INSERT INTO Seances (nom_activite, date, heure, nbr_places ) VALUES (seance_nomActivite, seance_date, seance_heure, seance_nbrPlace);
end //
DELIMITER ;



DROP PROCEDURE if exists insertion_categories;
DELIMITER //
CREATE PROCEDURE insertion_categories (  IN categorie_nom VARCHAR(50) )
BEGIN
    INSERT INTO Categories(nom) VALUES (categorie_nom);
end //
DELIMITER ;


DROP PROCEDURE if exists insertion_activites;
DELIMITER //
CREATE PROCEDURE insertion_activites (  IN activites_nom VARCHAR(50), IN activites_categorieId INT,IN activites_type VARCHAR(50), IN activites_coutOrganisation DOUBLE, IN activites_prixVente DOUBLE )
BEGIN
    INSERT INTO Activites (nom, id_categorie, type, cout_organisation, prix_vente) VALUES (activites_nom, activites_categorieId, activites_type, activites_coutOrganisation, activites_prixVente);
end //
DELIMITER ;



DROP PROCEDURE if exists insertion_adherents;
DELIMITER //
CREATE PROCEDURE insertion_adherents (  IN adherents_nom VARCHAR(50), IN adherents_prenom VARCHAR(50),IN adherents_adresse VARCHAR(50), IN adherents_dateNaissance DATE)
BEGIN
    INSERT INTO Adherents ( nom, prenom, adresse, date_naissance) VALUES (adherents_nom, adherents_prenom, adherents_adresse, adherents_dateNaissance);
end //
DELIMITER ;


/* Insertion des données */






CALL insertion_adherents( 'Messier', 'Manfred', '03 Homewood Place', '2005-02-08' );
CALL insertion_adherents ( 'Martensen', 'Tamqrah', '63587 Fairview Avenue', '2006-06-23' );
CALL insertion_adherents ( 'Mendoza', 'Annamarie', '3758 Texas Street', '2006-05-08');
CALL insertion_adherents ( 'Onions', 'Homere', '0 Hermina Avenue', '2006-09-08');
CALL insertion_adherents ( 'Applewhite', 'Dierdre', '4875 Lerdahl Center', '2005-06-19');
CALL insertion_adherents ( 'Muscroft', 'Coleman', '350 Esch Street', '2006-01-27');
CALL insertion_adherents ( 'Coupar', 'Cello', '49627 Arapahoe Parkway', '2005-02-13');
CALL insertion_adherents ( 'Astill', 'Abigael', '06 Esker Plaza', '2005-03-13');
CALL insertion_adherents ('Welsh', 'Siegfried', '1 Hanover Court', '2006-06-03');
CALL insertion_adherents ( 'Choak', 'Bette-ann', '82244 Johnson Lane', '2005-06-05');
/*
-- test trigger verifier_age
CALL insertion_adherents ( 'Malvin', 'Betty', '82234 Johnson Lane', '2012-06-05');
*/




CALL insertion_categories ('Sports');
CALL insertion_categories ('Sciences');
CALL insertion_categories ('Arts');
CALL insertion_categories ('Divertissements');
CALL insertion_categories ('Lectures');




CALL insertion_activites ('Canot-Camping', 1, 'Plein-air', 228.90, 138.09);
CALL insertion_activites ('Escalade', 1, 'Ludo-sportive', 169.02, 189.26);
CALL insertion_activites ('Peintures', 3, 'Peintures', 288.13, 209.42);
CALL insertion_activites ('Volleyball', 1, 'Activité d`intérieur', 202.60, 192.33);
CALL insertion_activites ('Cirque', 4, 'Spectacle', 297.79, 220.15);
CALL insertion_activites ('Équitation', 1, 'Plein air', 235.72, 214.00);
CALL insertion_activites ('Club de lecture', 5, 'Lecture', 153.43, 159.17);
CALL insertion_activites ('Tyrolienne', 4, 'Plein air', 178.32, 292.27);
CALL insertion_activites ('Via ferrata', 1, 'Plein air', 264.24, 275.55);
CALL insertion_activites ('Dentifrice d`éléphants', 2, 'Activité d`intérieur', 178.06, 127.03);






CALL insertion_seances ('Canot-Camping', '2024-11-21', '14:00:00' , 20);
CALL insertion_seances ('Escalade', '2024-12-04', '13:20:00' , 20);
CALL insertion_seances ('Canot-Camping', '2024-08-21', '13:30:00' , 20);
CALL insertion_seances ('Peintures', '2024-10-15', '12:20:00' , 20);
CALL insertion_seances ('Volleyball', '2024-01-17', '12:10:00' , 20);
CALL insertion_seances ('Tyrolienne', '2024-01-20', '11:20:00' , 20);
CALL insertion_seances ('Via ferrata', '2024-02-02', '12:20:00' , 20);
CALL insertion_seances ('Canot-Camping', '2024-11-22', '13:15:00' , 20);
CALL insertion_seances ('Dentifrice d`éléphants', '2024-11-24', '12:25:00' , 20);
CALL insertion_seances ('Canot-Camping', '2024-04-30', '10:40:00' , 20);
CALL insertion_seances ('Équitation', '2024-11-30', '11:45:00' , 20);
CALL insertion_seances ('Équitation', '2024-07-23', '13:10:00' , 20);
CALL insertion_seances ('Cirque', '2024-11-21', '10:20:00' , 20);
CALL insertion_seances ('Club de lecture', '2024-11-21', '09:20:00' , 20);


/* Ne pa oublier de changer les numéros d'identification des adhérents pour ceux nouvellement créés dans les insert inscriptions et evaluations*/
CALL insertion_inscriptions('BC-2005-675', 1);
CALL insertion_inscriptions('SW-2006-642', 1);
CALL insertion_inscriptions('TM-2006-469', 2);
CALL insertion_inscriptions('TM-2006-469', 3);


CALL insertion_evaluations('BC-2005-675', 1, 5);
CALL insertion_evaluations('SW-2006-642', 1, 4);
CALL insertion_evaluations('TM-2006-469', 2, 4);
CALL insertion_evaluations('TM-2006-469', 3, 4);


/* Création des fonctions */


DROP FUNCTION if exists NbrAdherentsParActivites;
DELIMITER //
CREATE FUNCTION NbrAdherentsParActivites (nomActivite VARCHAR(50))
RETURNS INT
BEGIN
    DECLARE nbrAdherents INT;

    -- Comptage des inscrits pour l'activité spécifiée
    SELECT COUNT(DISTINCT I.id_adherent)
    INTO nbrAdherents
    FROM Inscriptions I
    INNER JOIN Seances S ON I.id_seance = S.id
    WHERE S.nom_activite = nomActivite;

    RETURN nbrAdherents;
END //
DELIMITER ;



DROP FUNCTION if exists NbrAdherentsParMois;
DELIMITER //
CREATE FUNCTION NbrAdherentsParMois ( mois VARCHAR(50))
RETURNS INT
BEGIN
    DECLARE nbrAdherents INT;

    SELECT COUNT(DISTINCT I.id_adherent)
    INTO nbrAdherents
    FROM Inscriptions I
    INNER JOIN Seances S ON I.id_seance = S.id
    WHERE SUBSTRING(S.date, 6, 2) = mois;

    RETURN nbrAdherents;
END //
DELIMITER ;



DROP FUNCTION IF EXISTS NbrAdherentsInscritsAPLusieursActivites;
DELIMITER //
CREATE FUNCTION NbrAdherentsInscritsAPLusieursActivites()
RETURNS INT
BEGIN
    DECLARE nbrAdherents INT;

    SELECT COUNT(DISTINCT I.id_adherent)
    INTO nbrAdherents
    FROM Inscriptions I
    INNER JOIN Seances S ON I.id_seance = S.id
    GROUP BY I.id_adherent
    HAVING COUNT(DISTINCT S.nom_activite) > 1;

    RETURN nbrAdherents;
END //
DELIMITER ;


DROP FUNCTION IF EXISTS nbrTotal_Activites;
DELIMITER //
CREATE FUNCTION nbrTotal_Activites()
RETURNS INT
BEGIN

    DECLARE nbrActivites INT;

    SELECT COUNT(Activites.nom) into nbrActivites FROM Activites;

    RETURN nbrActivites;
END //
DELIMITER ;



DROP FUNCTION IF EXISTS nbrTotal_Adherents;
DELIMITER //
CREATE FUNCTION nbrTotal_Adherents()
RETURNS INT
BEGIN

    DECLARE nbrAdherents INT;

    SELECT COUNT(Adherents.no_identification) into nbrAdherents FROM Adherents;

    RETURN nbrAdherents;
END //
DELIMITER ;

DROP FUNCTION if exists moyenneNotesParActivite;
DELIMITER //
CREATE FUNCTION moyenneNotesParActivite(nomActivite VARCHAR(50))
RETURNS DOUBLE
BEGIN
    DECLARE moyenneNote DOUBLE;


    SELECT AVG(E.note)
    INTO moyenneNote
    FROM Evaluations E
    INNER JOIN Seances S ON E.id_seance = S.id
    WHERE S.nom_activite = nomActivite;

    IF moyenneNote IS NULL THEN
        SET moyenneNote = 0;
    END IF;

    RETURN moyenneNote;
END //
DELIMITER ;


DROP FUNCTION if exists nbrSeancesParActivite;
DELIMITER //
CREATE FUNCTION nbrSeancesParActivite (nomActivite VARCHAR(50)) RETURNS INT
BEGIN

    DECLARE nbrSeances INT;
    SELECT COUNT(Seances.id)
    INTO nbrSeances
    FROM Seances
    INNER JOIN Activites ON Seances.nom_activite = Activites.nom
    WHERE Activites.nom = nomActivite;

    RETURN nbrSeances;
END //
DELIMITER ;



DROP FUNCTION IF EXISTS moyenneAgeAdherents;
DELIMITER //
CREATE FUNCTION moyenneAgeAdherents()
RETURNS DOUBLE
BEGIN
    DECLARE moyenneAge DOUBLE;
    SELECT AVG(age) INTO moyenneAge FROM Adherents;
    RETURN moyenneAge;
END //
DELIMITER ;


DROP FUNCTION IF EXISTS activiteAvecPlusInscriptions;
DELIMITER //
CREATE FUNCTION activiteAvecPlusInscriptions()
RETURNS VARCHAR(50)
BEGIN
    DECLARE activitePopulaire VARCHAR(50);
    SELECT S.nom_activite
    INTO activitePopulaire
    FROM Seances S
    INNER JOIN Inscriptions I ON S.id = I.id_seance
    GROUP BY S.nom_activite
    ORDER BY COUNT(I.id_adherent) DESC
    LIMIT 1;
    RETURN activitePopulaire;
END //
DELIMITER ;

/*Création des vues */

/*Vue 1 Trouver un participant ayant le nombre de séances le plus élevé*/

DROP VIEW IF EXISTS Participant_PlusSeance;
CREATE VIEW Participant_PlusSeance AS
    SELECT A.no_identification, A.nom, A.prenom, COUNT(I.id_seance)
        AS nombre_seances
    FROM Inscriptions I
    JOIN Adherents A ON I.id_adherent = A.no_identification
    GROUP BY A.no_identification, A.nom, A.prenom
    ORDER BY nombre_seances DESC
    LIMIT 1;
/*Vue 2 Trouver le prix moyen par activité pour chaque participant  */

DROP VIEW IF EXISTS Moy_Prix_activite;
CREATE VIEW Moy_Prix_activite AS
    SELECT I.id_adherent, A.nom
        AS activite_nom, AVG(A.prix_vente) AS prix_moyen
    FROM Inscriptions I
    JOIN Seances S ON I.id_seance = S.id
    JOIN Activites A ON S.nom_activite = A.nom
    GROUP BY I.id_adherent, A.nom;

/*Vue 3  Afficher les notes d'appréciation pour chaque acticvité*/


DROP VIEW IF EXISTS Note_activite;
CREATE VIEW Note_activite AS
    SELECT A.nom AS activite_nom, E.note
        AS note_participant, S.id AS id_seance
    FROM Evaluations E
    INNER JOIN Seances S ON E.id_seance = S.id
    INNER JOIN Activites A ON S.nom_activite = A.nom
    ORDER BY activite_nom, S.id;
/*Vue 4 Afficher la moyenne des notes pour chaque activité */
DROP VIEW IF EXISTS Moyenne_Note;
CREATE VIEW Moyenne_Note AS
    SELECT A.nom AS activite_nom, AVG(E.note)
        AS moyenne
    FROM Evaluations E
    INNER JOIN Seances S ON E.id_seance = S.id
    INNER JOIN Activites A ON S.nom_activite = A.nom
    GROUP BY A.nom
    ORDER BY moyenne;

/*Vue 5 Afficher le nombre de participant par activité*/
DROP VIEW Nbr_Participants_Activite;
CREATE VIEW Nbr_Participants_Activite
    AS
   select nom,
    NbrAdherentsParActivites(nom) AS nbr_participants
FROM Activites;

/*Vue 6  Afficher le nombre de participants par mois*/
DROP VIEW if exists Nbr_Participants_Mois;
CREATE VIEW Nbr_Participants_Mois
    AS
   select DISTINCT(SUBSTRING(date, 6, 2)) AS mois,
    NbrAdherentsParMois(SUBSTRING(date, 6, 2)) AS nbr_participants
FROM Seances;

