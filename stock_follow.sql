-- phpMyAdmin SQL Dump
-- version 5.2.0
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Apr 24, 2023 at 06:46 PM
-- Server version: 10.4.27-MariaDB
-- PHP Version: 8.0.25

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `stock_follow`
--

-- --------------------------------------------------------

--
-- Table structure for table `category`
--

CREATE TABLE `category` (
  `ID` int(11) NOT NULL,
  `Name` varchar(50) NOT NULL DEFAULT '0'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_turkish_ci;

--
-- Dumping data for table `category`
--

INSERT INTO `category` (`ID`, `Name`) VALUES
(4, 'Bilgisayar'),
(5, 'Telefon'),
(6, 'Tablet'),
(7, 'Mobilya'),
(8, 'Mutfak'),
(9, 'Temizlik'),
(10, 'Spor'),
(11, 'Yiyecek'),
(12, 'İçecek'),
(13, 'Dondurma'),
(14, 'Atıştırmalık'),
(15, 'Bilgisayar Parçaları'),
(16, 'Bilgisayar Aksesuarları'),
(17, 'Telefon Aksesuarları'),
(18, 'Tablet Aksesuarları'),
(19, 'Okul Eşyaları'),
(20, 'Bilgisayar Bileşenleri'),
(21, 'Telefon Bileşenleri'),
(22, 'Oyun Konsolları'),
(23, 'Oyun Kodları'),
(24, 'Oyunlar'),
(25, 'Hediye Kartları'),
(26, 'Hediye Paketleri'),
(27, 'Kulaklıklar'),
(28, 'Mikrofonlar');

-- --------------------------------------------------------

--
-- Table structure for table `stocks`
--

CREATE TABLE `stocks` (
  `ID` int(11) NOT NULL,
  `name` varchar(128) NOT NULL DEFAULT '0',
  `brand` varchar(64) NOT NULL DEFAULT '0',
  `buyPrice` decimal(20,6) NOT NULL DEFAULT 0.000000,
  `sellPrice` decimal(20,6) NOT NULL DEFAULT 0.000000,
  `categoryID` int(11) NOT NULL DEFAULT 0,
  `count` int(11) NOT NULL DEFAULT 0,
  `createdDate` datetime NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_turkish_ci;

--
-- Dumping data for table `stocks`
--

INSERT INTO `stocks` (`ID`, `name`, `brand`, `buyPrice`, `sellPrice`, `categoryID`, `count`, `createdDate`) VALUES
(2, 'İphone 9', 'Apple', '2450000.000000', '9320000.000000', 4, 500, '2023-04-24 17:01:28'),
(4, 'Monster Monitör 32', 'Monster', '1000.000000', '2000.000000', 20, 100, '2023-04-24 19:32:10'),
(5, 'Philips Monitör 27', 'Philips', '1000.000000', '2000.000000', 20, 100, '2023-04-24 19:32:28'),
(6, 'Philips Monitör 24', 'Philips', '5491.000000', '7951.000000', 20, 100, '2023-04-24 19:32:47'),
(7, 'Philips Monitör 21', 'Philips', '5491.000000', '7951.000000', 20, 100, '2023-04-24 19:32:55'),
(8, 'Asus Monitör 21', 'Asus', '5491.000000', '7951.000000', 20, 100, '2023-04-24 19:33:04'),
(9, 'Asus Monitör 24', 'Asus', '5491.000000', '7951.000000', 20, 100, '2023-04-24 19:33:09'),
(10, 'Asus Monitör 27', 'Asus', '5491.000000', '7951.000000', 20, 100, '2023-04-24 19:33:14'),
(11, 'Asus Monitör 32', 'Asus', '5491.000000', '7951.000000', 20, 100, '2023-04-24 19:33:18'),
(12, 'MSI Monitör 32', 'MSI', '5491.000000', '7951.000000', 20, 100, '2023-04-24 19:33:46'),
(13, 'MSI Monitör 27', 'MSI', '5491.000000', '7951.000000', 20, 100, '2023-04-24 19:33:49'),
(14, 'MSI Monitör 21', 'MSI', '5491.000000', '7951.000000', 20, 100, '2023-04-24 19:33:52'),
(15, 'Monster Notebook Abra A7', 'Monster', '5491.000000', '7951.000000', 4, 100, '2023-04-24 19:34:27'),
(16, 'iPad 8', 'Apple', '5491.000000', '7951.000000', 6, 100, '2023-04-24 19:34:40'),
(17, 'iPad Pro', 'Apple', '5491.000000', '7951.000000', 6, 100, '2023-04-24 19:34:53'),
(18, 'Samsun Galaxy Tab X', 'Samsung', '5491.000000', '7951.000000', 6, 100, '2023-04-24 19:35:53'),
(19, 'Samsun Galaxy Tab Y', 'Samsung', '5491.000000', '7951.000000', 6, 100, '2023-04-24 19:35:58'),
(20, 'Koltuk Takımı', 'IKEA', '54911.000000', '79511.000000', 7, 100, '2023-04-24 19:36:12'),
(21, 'Airfryer', 'Philips', '10000.000000', '12500.000000', 8, 100, '2023-04-24 19:36:39'),
(22, 'Camsil', '-', '100.000000', '150.000000', 9, 100, '2023-04-24 19:36:53'),
(23, 'TAKE HIQ SMASH PRO', 'HIQ', '100.000000', '1000.000000', 10, 100, '2023-04-24 19:37:13'),
(24, 'MR. NO Cheddar Sandviç', 'DARDANEL', '30.000000', '60.000000', 11, 100, '2023-04-24 19:37:44'),
(25, 'Coca Cola', 'Coca Cola', '5.000000', '20.000000', 12, 100, '2023-04-24 19:38:02'),
(26, 'Magnum Badem', 'Algida', '5.000000', '22.000000', 13, 100, '2023-04-24 19:38:17'),
(27, 'Doritos Risk', 'Fritolay', '5.000000', '22.000000', 14, 100, '2023-04-24 19:38:32'),
(28, 'Playstation 5', 'Sony', '1000.000000', '20000.000000', 22, 100, '2023-04-24 19:38:54'),
(29, 'VALORANT E-PIN 5000VP', 'Sony', '10.000000', '500.000000', 23, 100, '2023-04-24 19:39:14'),
(30, 'Hediya Kartı 1000TL', '-', '1.000000', '1000.000000', 25, 100, '2023-04-24 19:39:52'),
(31, 'Hediya Kartı 2000TL', '-', '1.000000', '2000.000000', 25, 100, '2023-04-24 19:39:55'),
(32, 'Hediya Kartı 3000TL', '-', '1.000000', '3000.000000', 25, 100, '2023-04-24 19:39:59'),
(33, 'Hediya Paketi', '-', '10.000000', '30.000000', 26, 100, '2023-04-24 19:40:12'),
(34, 'Airpods PRO 2.Nesil', 'Apple', '1000.000000', '5500.000000', 27, 100, '2023-04-24 19:40:34'),
(35, 'BM 500', 'Justvoice', '200.000000', '480.000000', 28, 100, '2023-04-24 19:40:55');

-- --------------------------------------------------------

--
-- Table structure for table `users`
--

CREATE TABLE `users` (
  `ID` int(11) NOT NULL,
  `username` varchar(50) NOT NULL DEFAULT '',
  `name` varchar(50) NOT NULL DEFAULT '',
  `surname` varchar(50) NOT NULL DEFAULT '',
  `mail` varchar(128) NOT NULL DEFAULT '',
  `password` varchar(68) NOT NULL DEFAULT '',
  `admin` tinyint(4) NOT NULL DEFAULT 0,
  `created` datetime NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_turkish_ci;

--
-- Dumping data for table `users`
--

INSERT INTO `users` (`ID`, `username`, `name`, `surname`, `mail`, `password`, `admin`, `created`) VALUES
(1, 'dmrc', 'Ömer Faruk', 'DEMİRCİOĞLU', 'dmrcogluomer@hotmail.com', '9dee06ce77a403208f54308bd22c84342bb025473f2b2e4e0bdf39f7bb1bbfce', 5, '2023-03-30 03:15:52');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `category`
--
ALTER TABLE `category`
  ADD PRIMARY KEY (`ID`);

--
-- Indexes for table `stocks`
--
ALTER TABLE `stocks`
  ADD PRIMARY KEY (`ID`);

--
-- Indexes for table `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`ID`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `category`
--
ALTER TABLE `category`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=29;

--
-- AUTO_INCREMENT for table `stocks`
--
ALTER TABLE `stocks`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=36;

--
-- AUTO_INCREMENT for table `users`
--
ALTER TABLE `users`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=12;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
