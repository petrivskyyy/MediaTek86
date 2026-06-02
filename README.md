# MediaTek86 — Application de gestion des absences du personnel

Application de bureau développée en **C# / Windows Forms (.NET Framework)** dans le cadre de l'Atelier de Professionnalisation 2 du BTS SIO (option SLAM). Elle permet au responsable du personnel de MediaTek86 de gérer le personnel et leurs absences. L'application reprend la structure et la logique de l'application Habilitations (architecture MVC, classe BddManager, couche dal, modèle métier).

## Contexte et mission

MediaTek86 est un réseau de médiathèques. Le responsable du personnel a besoin d'une application lui permettant de gérer les membres du personnel (ajout, modification, suppression) et de suivre leurs absences. L'accès est protégé par une authentification (login + mot de passe hashé en SHA2 256).

## Fonctionnalités (8 cas d'utilisation)

1. Se connecter
2. Ajouter un personnel
3. Supprimer un personnel
4. Modifier un personnel
5. Afficher les absences d'un personnel
6. Ajouter une absence (avec contrôle du chevauchement)
7. Supprimer une absence
8. Modifier une absence (avec contrôle du chevauchement)

## Technologies

- Langage : C#
- Framework : .NET Framework (Windows Forms)
- IDE : Visual Studio
- SGBD : MySQL (WampServer)
- Connecteur : MySql.Data (NuGet)
- Modélisation : Looping
- Versioning : Git / GitHub

## Architecture MVC

L'application est organisée en packages :
- **bddmanager** : BddManager.cs (classe singleton de connexion)
- **dal** : Access.cs, PersonnelAccess.cs, AbsenceAccess.cs (couche d'accès aux données)
- **modele** : Service, Personnel, Motif, Absence (classes métier)
- **controleur** : FrmConnexionControleur, FrmAccueilControleur
- **vue** : FrmConnexion, FrmAccueil, FrmPersonnel, FrmAbsence

## Modèle de données (MCD)

5 tables : service, personnel, motif, absence (clé primaire composée idpersonnel + datedebut), responsable (login + pwd, une seule ligne).

*(MCD : captures/mcd.png)*

## Installation

1. Démarrer WampServer.
2. Dans phpMyAdmin, exécuter le script `mediatek86.sql` (création des tables, de l'utilisateur d'accès et des données de test).
3. Ouvrir la solution dans Visual Studio, restaurer le paquet NuGet MySql.Data.
4. Vérifier la chaîne de connexion dans App.config.
5. Compiler et lancer (F5). Un installateur (.msi) est disponible dans les Releases.

## Connexion

- Login : **admin**
- Mot de passe : **motdepasse**

## Interfaces

*(captures/connexion.png, captures/accueil.png, captures/personnel.png, captures/absence.png)*

## Diagramme de paquetages

*(captures/diagramme_paquetages.png)*

## Étapes de construction (commits)

| Commit | Description |
|--------|-------------|
| #1 | Initialisation du projet et structure MVC |
| #2 | Codage de la partie Vue (interfaces) |
| #3 | Ajout BddManager, couche dal et modèle métier |
| #4 | Fonctionnalités CU1 à CU8 et connexion BDD |
| #5 | Génération de la documentation technique |
| #6 | Création de l'installateur |

## Liens

- Vidéo de démonstration : *(lien à venir)*
- Page portfolio : *(lien à venir)*
- Compte rendu d'activité (PDF) : *(lien à venir)*

## Auteur

Étudiant BTS SIO (SLAM) — Atelier de Professionnalisation 2.
