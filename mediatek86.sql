-- ============================================================
--  MediaTek86 - Gestion des absences du personnel
--  Script complet : création BDD + utilisateur + données
--  SGBD : MySQL
-- ============================================================

DROP DATABASE IF EXISTS mediatek86;
CREATE DATABASE mediatek86 CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci;
USE mediatek86;

-- ------------------------------------------------------------
--  Création des tables
-- ------------------------------------------------------------

CREATE TABLE service (
    idservice INT NOT NULL AUTO_INCREMENT,
    nom VARCHAR(50) NOT NULL,
    CONSTRAINT pk_service PRIMARY KEY (idservice)
);

CREATE TABLE personnel (
    idpersonnel INT NOT NULL AUTO_INCREMENT,
    nom VARCHAR(50) NOT NULL,
    prenom VARCHAR(50) NOT NULL,
    tel VARCHAR(15),
    mail VARCHAR(100),
    idservice INT NOT NULL,
    CONSTRAINT pk_personnel PRIMARY KEY (idpersonnel),
    CONSTRAINT fk_personnel_service FOREIGN KEY (idservice)
        REFERENCES service (idservice)
);

CREATE TABLE motif (
    idmotif INT NOT NULL AUTO_INCREMENT,
    libelle VARCHAR(50) NOT NULL,
    CONSTRAINT pk_motif PRIMARY KEY (idmotif)
);

CREATE TABLE absence (
    idpersonnel INT NOT NULL,
    datedebut DATETIME NOT NULL,
    datefin DATETIME NOT NULL,
    idmotif INT NOT NULL,
    CONSTRAINT pk_absence PRIMARY KEY (idpersonnel, datedebut),
    CONSTRAINT fk_absence_personnel FOREIGN KEY (idpersonnel)
        REFERENCES personnel (idpersonnel),
    CONSTRAINT fk_absence_motif FOREIGN KEY (idmotif)
        REFERENCES motif (idmotif)
);

CREATE TABLE responsable (
    login VARCHAR(64) NOT NULL,
    pwd VARCHAR(64) NOT NULL
);

-- ------------------------------------------------------------
--  Création de l'utilisateur ayant accès à la base
-- ------------------------------------------------------------
CREATE USER IF NOT EXISTS 'mediatekuser'@'localhost' IDENTIFIED BY 'mediatekpwd';
GRANT SELECT, INSERT, UPDATE, DELETE ON mediatek86.* TO 'mediatekuser'@'localhost';
FLUSH PRIVILEGES;

-- ------------------------------------------------------------
--  Alimentation des tables
-- ------------------------------------------------------------

-- responsable (login : admin / mot de passe : motdepasse, hashé en SHA2 256)
INSERT INTO responsable (login, pwd)
VALUES ('admin', SHA2('motdepasse', 256));

-- service
INSERT INTO service (idservice, nom) VALUES
(1, 'administratif'),
(2, 'médiation culturelle'),
(3, 'prêt');

-- motif
INSERT INTO motif (idmotif, libelle) VALUES
(1, 'vacances'),
(2, 'maladie'),
(3, 'motif familial'),
(4, 'congé parental');

-- personnel (10 exemples)
INSERT INTO personnel (idpersonnel, nom, prenom, tel, mail, idservice) VALUES
(1,  'Dupont',   'Marie',    '0612345678', 'marie.dupont@mediatek86.fr',   1),
(2,  'Martin',   'Lucas',    '0623456789', 'lucas.martin@mediatek86.fr',   3),
(3,  'Bernard',  'Sophie',   '0634567890', 'sophie.bernard@mediatek86.fr', 2),
(4,  'Petit',    'Thomas',   '0645678901', 'thomas.petit@mediatek86.fr',   1),
(5,  'Robert',   'Julie',    '0656789012', 'julie.robert@mediatek86.fr',   3),
(6,  'Richard',  'Antoine',  '0667890123', 'antoine.richard@mediatek86.fr',2),
(7,  'Durand',   'Camille',  '0678901234', 'camille.durand@mediatek86.fr', 1),
(8,  'Moreau',   'Hugo',     '0689012345', 'hugo.moreau@mediatek86.fr',    3),
(9,  'Laurent',  'Emma',     '0690123456', 'emma.laurent@mediatek86.fr',   2),
(10, 'Simon',    'Nathan',   '0601234567', 'nathan.simon@mediatek86.fr',   1);

-- absence (50 exemples)
INSERT INTO absence (idpersonnel, datedebut, datefin, idmotif) VALUES
(1, '2025-12-01 00:00:00', '2025-12-04 00:00:00', 3),
(7, '2024-09-04 00:00:00', '2024-09-18 00:00:00', 4),
(1, '2024-04-12 00:00:00', '2024-04-14 00:00:00', 2),
(7, '2025-02-16 00:00:00', '2025-03-01 00:00:00', 1),
(5, '2025-03-27 00:00:00', '2025-04-12 00:00:00', 3),
(7, '2025-07-23 00:00:00', '2025-07-30 00:00:00', 2),
(1, '2025-05-11 00:00:00', '2025-05-28 00:00:00', 3),
(6, '2025-10-14 00:00:00', '2025-10-28 00:00:00', 3),
(3, '2025-11-17 00:00:00', '2025-12-02 00:00:00', 4),
(1, '2024-12-01 00:00:00', '2024-12-07 00:00:00', 1),
(7, '2025-04-18 00:00:00', '2025-05-09 00:00:00', 1),
(9, '2024-01-11 00:00:00', '2024-01-26 00:00:00', 2),
(8, '2024-09-10 00:00:00', '2024-09-16 00:00:00', 3),
(7, '2025-12-22 00:00:00', '2025-12-30 00:00:00', 4),
(9, '2024-06-02 00:00:00', '2024-06-12 00:00:00', 1),
(6, '2025-01-09 00:00:00', '2025-01-18 00:00:00', 3),
(4, '2025-05-19 00:00:00', '2025-05-29 00:00:00', 4),
(8, '2025-07-02 00:00:00', '2025-07-05 00:00:00', 2),
(3, '2024-07-01 00:00:00', '2024-07-10 00:00:00', 1),
(7, '2025-07-28 00:00:00', '2025-08-06 00:00:00', 2),
(10, '2025-11-25 00:00:00', '2025-11-30 00:00:00', 4),
(1, '2025-07-05 00:00:00', '2025-07-12 00:00:00', 2),
(10, '2025-03-25 00:00:00', '2025-04-06 00:00:00', 1),
(7, '2024-01-04 00:00:00', '2024-01-07 00:00:00', 4),
(5, '2025-03-09 00:00:00', '2025-03-14 00:00:00', 2),
(6, '2024-02-26 00:00:00', '2024-03-08 00:00:00', 1),
(1, '2025-10-23 00:00:00', '2025-11-11 00:00:00', 2),
(4, '2025-09-20 00:00:00', '2025-10-01 00:00:00', 2),
(9, '2025-04-16 00:00:00', '2025-04-21 00:00:00', 1),
(4, '2024-07-20 00:00:00', '2024-08-04 00:00:00', 2),
(8, '2025-07-08 00:00:00', '2025-07-19 00:00:00', 2),
(4, '2024-06-05 00:00:00', '2024-06-23 00:00:00', 3),
(9, '2025-10-23 00:00:00', '2025-10-26 00:00:00', 4),
(2, '2025-07-27 00:00:00', '2025-08-12 00:00:00', 3),
(3, '2024-02-08 00:00:00', '2024-02-18 00:00:00', 3),
(3, '2024-09-15 00:00:00', '2024-09-28 00:00:00', 3),
(7, '2025-01-05 00:00:00', '2025-01-20 00:00:00', 1),
(3, '2025-11-18 00:00:00', '2025-11-29 00:00:00', 2),
(9, '2025-03-06 00:00:00', '2025-03-11 00:00:00', 1),
(7, '2025-04-26 00:00:00', '2025-05-02 00:00:00', 4),
(4, '2025-10-28 00:00:00', '2025-11-08 00:00:00', 2),
(2, '2025-05-04 00:00:00', '2025-05-19 00:00:00', 1),
(1, '2025-08-15 00:00:00', '2025-08-26 00:00:00', 2),
(10, '2024-10-25 00:00:00', '2024-11-08 00:00:00', 1),
(4, '2024-06-25 00:00:00', '2024-07-05 00:00:00', 2),
(2, '2024-02-10 00:00:00', '2024-03-01 00:00:00', 3),
(7, '2025-12-19 00:00:00', '2025-12-27 00:00:00', 4),
(6, '2024-02-28 00:00:00', '2024-03-14 00:00:00', 4),
(1, '2024-08-14 00:00:00', '2024-08-29 00:00:00', 1),
(8, '2025-08-07 00:00:00', '2025-08-18 00:00:00', 4);
