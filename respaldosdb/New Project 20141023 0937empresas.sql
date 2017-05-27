-- MySQL Administrator dump 1.4
--
-- ------------------------------------------------------
-- Server version	5.5.17


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;


--
-- Create schema db_servicesempresas
--

CREATE DATABASE IF NOT EXISTS db_servicesempresas;
USE db_servicesempresas;

--
-- Definition of table `tblempresas`
--

DROP TABLE IF EXISTS `tblempresas`;
CREATE TABLE `tblempresas` (
  `idempresa` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `nombreempresa` varchar(200) NOT NULL,
  `db` varchar(45) NOT NULL,
  `pass` blob NOT NULL,
  `servidor` varchar(250) NOT NULL,
  `usuario` varchar(45) NOT NULL,
  `esdefault` tinyint(3) unsigned NOT NULL,
  PRIMARY KEY (`idempresa`)
) ENGINE=InnoDB AUTO_INCREMENT=20 DEFAULT CHARSET=utf8;

--
-- Dumping data for table `tblempresas`
--

/*!40000 ALTER TABLE `tblempresas` DISABLE KEYS */;
INSERT INTO `tblempresas` (`idempresa`,`nombreempresa`,`db`,`pass`,`servidor`,`usuario`,`esdefault`) VALUES 
 (1,'Principal','db_services',0xE15AF6D2900A85B338F0156169B3096E,'localhost','root',1),
 (4,'Vacia','db_services2',0xE15AF6D2900A85B338F0156169B3096E,'localhost','root',0),
 (5,'FRUTI','db_servicesfruti',0xE15AF6D2900A85B338F0156169B3096E,'localhost','root',0),
 (6,'Agronegocios','db_servicesagro',0xE15AF6D2900A85B338F0156169B3096E,'localhost','root',0),
 (7,'MEZa','db_servicesmeza',0xE15AF6D2900A85B338F0156169B3096E,'locahost','root',0),
 (8,'Prueba','db_servicesprueba',0xE15AF6D2900A85B338F0156169B3096E,'localhost','root',1),
 (9,'Vacia2','db_servicesv2',0xE15AF6D2900A85B338F0156169B3096E,'localhost','root',0),
 (10,'Muro','db_servicesmyrno',0xE15AF6D2900A85B338F0156169B3096E,'lo','root',0),
 (11,'MATERIALES','db_servicesnue',0xE15AF6D2900A85B338F0156169B3096E,'localhost','root',0),
 (12,'MUROS2','db_servicesnue2',0xE060F21ACEBC9D48,'a','a',0),
 (13,'Virginia','db_servicesvirginia',0x3A9885E6AD9EC43B,'q','q',0),
 (14,'Sya','db_servicessya',0x3A9885E6AD9EC43B,'q','q',0),
 (15,'PEPSI','db_servicespepsi',0xF8FF5F1632806AB0,'locahost','x',0),
 (16,'Florez','db_servicesf',0xE15AF6D2900A85B338F0156169B3096E,'localhost','root',0),
 (17,'Plasticos','db_servicesplasticos',0xE15AF6D2900A85B338F0156169B3096E,'localhost','root',0),
 (18,'TIPRO','db_servicestipro',0xE15AF6D2900A85B338F0156169B3096E,'localhost','root',0),
 (19,'Frutilandia2','db_servicesfruti2',0xE15AF6D2900A85B338F0156169B3096E,'localhost','root',0);
/*!40000 ALTER TABLE `tblempresas` ENABLE KEYS */;




/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
