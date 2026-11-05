-- ============================================================
-- 1. CRÉATION DES TABLES
-- ============================================================

CREATE TABLE PARTIE (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    nom VARCHAR(100) NOT NULL
);

CREATE TABLE TOURNOI (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    division VARCHAR(50) NOT NULL,
    id_partie INTEGER NOT NULL,
    FOREIGN KEY (id_partie) REFERENCES PARTIE(id) ON DELETE CASCADE
);

CREATE TABLE EQUIPE (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    nom VARCHAR(100) NOT NULL,
    ville_origine VARCHAR(100),
    est_rival BOOLEAN DEFAULT 0
);

CREATE TABLE JOUEUR (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    nom VARCHAR(50) NOT NULL,
    prenom VARCHAR(50) NOT NULL,
    attaque INTEGER DEFAULT 50,
    defense INTEGER DEFAULT 50,
    passe INTEGER DEFAULT 50,
    energie INTEGER DEFAULT 100,
    blessure BOOLEAN DEFAULT 0,
    prix DECIMAL(10,2) DEFAULT 0.00,
    note DECIMAL(3,1) DEFAULT 0.0,
    id_equipe INTEGER NULL,
    FOREIGN KEY (id_equipe) REFERENCES EQUIPE(id) ON DELETE SET NULL
);

CREATE TABLE COACH (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    nom VARCHAR(50) NOT NULL,
    prenom VARCHAR(50) NOT NULL,
    boost_attaque INTEGER DEFAULT 0,
    boost_defense INTEGER DEFAULT 0,
    boost_passe INTEGER DEFAULT 0,
    prix DECIMAL(10,2) DEFAULT 0.00,
    id_equipe INTEGER NULL,
    FOREIGN KEY (id_equipe) REFERENCES EQUIPE(id) ON DELETE SET NULL
);

CREATE TABLE MATCH (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    date_match DATE NOT NULL,
    score_equipe1 INTEGER DEFAULT 0,
    score_equipe2 INTEGER DEFAULT 0,
    vainqueur VARCHAR(100) NULL,
    perdant VARCHAR(100) NULL,
    id_tournoi INTEGER NOT NULL,
    id_equipe_domicile INTEGER NOT NULL,
    id_equipe_exterieur INTEGER NOT NULL,
    FOREIGN KEY (id_tournoi) REFERENCES TOURNOI(id) ON DELETE CASCADE,
    FOREIGN KEY (id_equipe_domicile) REFERENCES EQUIPE(id),
    FOREIGN KEY (id_equipe_exterieur) REFERENCES EQUIPE(id)
);


-- ============================================================
-- 2. INSERTION DE DONNÉES TEST
-- ============================================================

-- Insertion de 4 Équipes[cite: 13]
INSERT INTO EQUIPE (id, nom, ville_origine, est_rival) VALUES
(1, 'FC Rouen', 'Rouen', 0),
(2, 'Le Havre AC', 'Le Havre', 1),
(3, 'US Quevilly', 'Petit-Quevilly', 0),
(4, 'Évreux FC', 'Évreux', 0);

-- Insertion de 4 Coachs[cite: 13]
INSERT INTO COACH (id, nom, prenom, boost_attaque, boost_defense, boost_passe, prix, id_equipe) VALUES
(1, 'Dupont', 'Michel', 5, 2, 3, 15000.00, 1),
(2, 'Le Gall', 'Antoine', 3, 6, 2, 18000.00, 2),
(3, 'Benali', 'Youssef', 4, 4, 5, 20000.00, 3),
(4, 'Moreau', 'Jean-Paul', 2, 3, 4, 12000.00, 4);

-- Insertion de 30 Joueurs[cite: 13]
INSERT INTO JOUEUR (nom, prenom, attaque, defense, passe, energie, blessure, prix, note, id_equipe) VALUES
-- FC Rouen (7 joueurs)
('Martin', 'Lucas', 78, 45, 70, 95, 0, 45000.00, 7.5, 1),
('Bernard', 'Thomas', 50, 82, 60, 90, 0, 50000.00, 7.8, 1),
('Petit', 'Hugo', 85, 30, 65, 88, 0, 60000.00, 8.1, 1),
('Robert', 'Alexandre', 40, 79, 72, 92, 0, 42000.00, 7.2, 1),
('Richard', 'Maxime', 65, 68, 84, 96, 0, 55000.00, 8.0, 1),
('Durand', 'Enzo', 72, 55, 78, 89, 0, 48000.00, 7.4, 1),
('Dubois', 'Gabriel', 30, 85, 50, 91, 0, 52000.00, 7.6, 1),

-- Le Havre AC (7 joueurs)
('Morel', 'Mathis', 88, 35, 75, 94, 0, 75000.00, 8.4, 2),
('Laurent', 'Clément', 45, 84, 68, 93, 0, 58000.00, 7.9, 2),
('Simon', 'Léo', 70, 70, 80, 90, 0, 62000.00, 8.0, 2),
('Michel', 'Julien', 82, 40, 62, 85, 0, 54000.00, 7.7, 2),
('Lefebvre', 'Nathan', 35, 80, 55, 95, 0, 46000.00, 7.3, 2),
('Leroy', 'Romain', 68, 62, 85, 88, 0, 61000.00, 8.2, 2),
('Roux', 'Antoine', 75, 50, 72, 92, 0, 50000.00, 7.5, 2),

-- US Quevilly (6 joueurs)
('David', 'Florian', 74, 48, 69, 87, 0, 38000.00, 7.1, 3),
('Bertrand', 'Alexis', 42, 76, 64, 91, 0, 40000.00, 7.3, 3),
('Fournier', 'Valentin', 60, 65, 77, 89, 0, 41000.00, 7.2, 3),
('Girard', 'Arthur', 79, 32, 61, 84, 0, 43000.00, 7.4, 3),
('Bonnet', 'Louis', 38, 78, 58, 93, 0, 39000.00, 7.0, 3),
('Dupuis', 'Raphaël', 66, 60, 73, 90, 0, 42000.00, 7.3, 3),

-- Évreux FC (6 joueurs)
('Fontaine', 'Sébastien', 71, 52, 67, 86, 0, 35000.00, 6.9, 4),
('Chevalier', 'Benjamin', 40, 75, 61, 88, 0, 37000.00, 7.1, 4),
('Robin', 'Nicolas', 63, 64, 75, 92, 0, 39000.00, 7.2, 4),
('Masson', 'Paul', 77, 30, 60, 83, 0, 41000.00, 7.3, 4),
('Sanchez', 'David', 35, 77, 52, 94, 0, 36000.00, 6.8, 4),
('Gérard', 'Mathieu', 68, 58, 70, 89, 0, 38000.00, 7.0, 4),

-- Agents Libres (4 joueurs)[cite: 13]
('Mercier', 'Adrien', 81, 42, 79, 90, 0, 55000.00, 7.8, NULL),
('Blanc', 'Kevin', 48, 81, 63, 85, 0, 47000.00, 7.5, NULL),
('Guerin', 'Théo', 73, 50, 71, 88, 0, 44000.00, 7.3, NULL),
('Boyer', 'Kilian', 62, 66, 76, 91, 0, 40000.00, 7.1, NULL);