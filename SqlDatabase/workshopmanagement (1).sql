-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Erstellungszeit: 11. Mrz 2024 um 08:55
-- Server-Version: 10.4.32-MariaDB
-- PHP-Version: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Datenbank: `workshopmanagement`
--

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `inventory`
--

CREATE TABLE `inventory` (
  `material_id` int(11) DEFAULT NULL,
  `amount` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `material`
--

CREATE TABLE `material` (
  `id` int(11) NOT NULL,
  `name` varchar(100) DEFAULT NULL,
  `description` varchar(500) DEFAULT NULL,
  `type` varchar(32) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Daten für Tabelle `material`
--

INSERT INTO `material` (`id`, `name`, `description`, `type`) VALUES
(1, 'fewewf', 'ewfw', 'awf');

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `project`
--

CREATE TABLE `project` (
  `id` int(11) NOT NULL,
  `name` varchar(100) DEFAULT NULL,
  `client` varchar(100) DEFAULT NULL,
  `description` varchar(500) DEFAULT NULL,
  `startpoint` timestamp NULL DEFAULT NULL,
  `endpoint` timestamp NULL DEFAULT NULL,
  `estimated_costs` float DEFAULT NULL,
  `costs` float DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Daten für Tabelle `project`
--

INSERT INTO `project` (`id`, `name`, `client`, `description`, `startpoint`, `endpoint`, `estimated_costs`, `costs`) VALUES
(1, 'dwq', 'dwq', 'dwq', '2024-03-25 10:20:19', '2024-03-27 10:20:19', 244, 326);

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `project_file`
--

CREATE TABLE `project_file` (
  `id` int(11) NOT NULL,
  `project_id` int(11) NOT NULL,
  `file` varchar(150) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Daten für Tabelle `project_file`
--

INSERT INTO `project_file` (`id`, `project_id`, `file`) VALUES
(1, 1, 'few'),
(2, 1, 'rwgdw');

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `project_material`
--

CREATE TABLE `project_material` (
  `id` int(11) NOT NULL,
  `project_id` int(11) DEFAULT NULL,
  `material_id` int(11) NOT NULL,
  `amount` int(16) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Daten für Tabelle `project_material`
--

INSERT INTO `project_material` (`id`, `project_id`, `material_id`, `amount`) VALUES
(2, 1, 1, 45),
(3, 1, 1, 12);

--
-- Indizes der exportierten Tabellen
--

--
-- Indizes für die Tabelle `inventory`
--
ALTER TABLE `inventory`
  ADD UNIQUE KEY `material_id` (`material_id`);

--
-- Indizes für die Tabelle `material`
--
ALTER TABLE `material`
  ADD PRIMARY KEY (`id`);

--
-- Indizes für die Tabelle `project`
--
ALTER TABLE `project`
  ADD PRIMARY KEY (`id`);

--
-- Indizes für die Tabelle `project_file`
--
ALTER TABLE `project_file`
  ADD PRIMARY KEY (`id`),
  ADD KEY `project_id` (`project_id`);

--
-- Indizes für die Tabelle `project_material`
--
ALTER TABLE `project_material`
  ADD PRIMARY KEY (`id`),
  ADD KEY `project_id` (`project_id`),
  ADD KEY `material_id` (`material_id`);

--
-- AUTO_INCREMENT für exportierte Tabellen
--

--
-- AUTO_INCREMENT für Tabelle `material`
--
ALTER TABLE `material`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT für Tabelle `project_material`
--
ALTER TABLE `project_material`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;

--
-- Constraints der exportierten Tabellen
--

--
-- Constraints der Tabelle `inventory`
--
ALTER TABLE `inventory`
  ADD CONSTRAINT `inventory_ibfk_1` FOREIGN KEY (`material_id`) REFERENCES `material` (`id`) ON DELETE CASCADE ON UPDATE NO ACTION;

--
-- Constraints der Tabelle `project_file`
--
ALTER TABLE `project_file`
  ADD CONSTRAINT `project_file_ibfk_1` FOREIGN KEY (`project_id`) REFERENCES `project` (`id`) ON DELETE CASCADE ON UPDATE NO ACTION;

--
-- Constraints der Tabelle `project_material`
--
ALTER TABLE `project_material`
  ADD CONSTRAINT `project_material_ibfk_1` FOREIGN KEY (`material_id`) REFERENCES `material` (`id`) ON DELETE CASCADE ON UPDATE NO ACTION,
  ADD CONSTRAINT `project_material_ibfk_2` FOREIGN KEY (`project_id`) REFERENCES `project` (`id`) ON DELETE SET NULL ON UPDATE NO ACTION;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
