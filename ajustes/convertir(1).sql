#importar clientes
insert into db_services.tblclientes
select id,concat(nombre,' ',apellidos),direccion,
telefono,email,'',numero,rfc,giro,ciudad,cp,estado,pais,'','','','','',noexterior,
nointerior,colonia,municipio,referencia,'','','','','',0,limitecredito,diascredito,
0,0,0,1,1,curp,0,0,0 from facturacion_nue.clientes;
#ver 9.0.25 rev 28
insert into db_services2.tblclientes
select id,concat(nombre,' ',apellidos),direccion,
telefono,email,'',numero,rfc,giro,ciudad,cp,estado,pais,'','','','','',noexterior,
nointerior,colonia,municipio,referencia,'','','','','',0,limitecredito,diascredito,
0,0,0,1,1,curp,0,0,0,'','','',0,0,0,0,0,'' from facturacion_nue.clientes;

#importar proveedores
insert into db_services.tblproveedores select id,concat(nombre,' ',apellidos),direccion,
telefono,email,'',numero,rfc,'',ciudad,cp,estado,pais,noexterior,
nointerior,colonia,municipio,referencia,diascredito,limitecredito from facturacion_nue.proveedores;

#importar clasificaciones
insert into db_services.tblinventarioclasificaciones select id+1,descripcion,codigo from facturacion_nue.clasificaciones where length(codigo)=3;
insert into db_services.tblinventarioclasificaciones2
select c.id+1,c.descripcion,mid(c.codigo,4,3),(select ci.id+1 from facturacion_nue.clasificaciones ci where ci.codigo=left(c.codigo,3)) from facturacion_nue.clasificaciones c where length(codigo)=6;
insert into db_services.tblinventarioclasificaciones3
select c.id+1,c.descripcion,mid(c.codigo,7,3),(select ci.id+1 from facturacion_nue.clasificaciones ci where ci.codigo=left(c.codigo,6)) from facturacion_nue.clasificaciones c where length(codigo)=9;

#importar articulos
alter table db_services.tblinventario modify column nombre varchar(250) not null;
insert into db_services.tblinventario
select a.id+1,descripcion,1,7,1,7,comentario,codigo,
(select id+1 from facturacion_nue.clasificaciones where codigo=left(a.clasificacion,3)),
reorden,facturacion_nue.promedio(a.id,now()),2,noparte,
if(length(a.clasificacion)=6,(select id+1 from facturacion_nue.clasificaciones where codigo=left(a.clasificacion,6)),1),
if(length(a.clasificacion)=9,(select id+1 from facturacion_nue.clasificaciones where codigo=left(a.clasificacion,9)),1),usaseries,0,inventariable,
(select if(iva,16,0) from facturacion_nue.clasificaciones where codigo=a.clasificacion),
0,codigo2,'',neto
from facturacion_nue.articulos a;
#ver 9.0.25 rev 28
insert into db_services2.tblinventario
select a.id+1,descripcion,1,7,1,7,comentario,codigo,
(select id+1 from facturacion_nue.clasificaciones where codigo=left(a.clasificacion,3)),
reorden,facturacion_nue.promedio(a.id,now()),2,noparte,
if(length(a.clasificacion)=6,(select id+1 from facturacion_nue.clasificaciones where codigo=left(a.clasificacion,6)),1),
if(length(a.clasificacion)=9,(select id+1 from facturacion_nue.clasificaciones where codigo=left(a.clasificacion,9)),1),usaseries,0,inventariable,
(select if(iva,16,0) from facturacion_nue.clasificaciones where codigo=a.clasificacion),
0,codigo2,'',neto,'',0,0,0,0,0,0,0,0,0,0,''
from facturacion_nue.articulos a;

#establece datos de empresa

update db_services.tblsucursales set nombre=(select valor from facturacion_nue.configuracion where nombre='empresa'),
direccion=(select valor from facturacion_nue.configuracion where nombre='calle'),
telefono=(select valor from facturacion_nue.configuracion where nombre='telefonos'),
email=(select valor from facturacion_nue.configuracion where nombre='correo'),
contacto=(select valor from facturacion_nue.configuracion where nombre='titular'),
rfc=(select valor from facturacion_nue.configuracion where nombre='rfc'),
ciudad=(select valor from facturacion_nue.configuracion where nombre='localidad'),
cp=(select valor from facturacion_nue.configuracion where nombre='cp'),
estado=(select valor from facturacion_nue.configuracion where nombre='estado'),
pais=(select valor from facturacion_nue.configuracion where nombre='pais'),
direccion2=(select valor from facturacion_nue.configuracion where nombre='callesuc'),
ciudad2=(select valor from facturacion_nue.configuracion where nombre='localidadsuc'),
cp2=(select valor from facturacion_nue.configuracion where nombre='cpsuc'),
estado2=(select valor from facturacion_nue.configuracion where nombre='estadosuc'),
pais2=(select valor from facturacion_nue.configuracion where nombre='paissuc'),
noexterior=(select valor from facturacion_nue.configuracion where nombre='noexteriorsuc'),
nointerior=(select valor from facturacion_nue.configuracion where nombre='nointeriorsuc'),
colonia=(select valor from facturacion_nue.configuracion where nombre='coloniasuc'),
municipio=(select valor from facturacion_nue.configuracion where nombre='municipiosuc'),
referenciadomicilio=(select valor from facturacion_nue.configuracion where nombre='referenciasuc'),
noexterior2=(select valor from facturacion_nue.configuracion where nombre='noexteriorsuc'),
nointerior2=(select valor from facturacion_nue.configuracion where nombre='nointeriorsuc'),
colonia2=(select valor from facturacion_nue.configuracion where nombre='coloniasuc'),
municipio2=(select valor from facturacion_nue.configuracion where nombre='municipiosuc'),
referenciadomicilio2=(select valor from facturacion_nue.configuracion where nombre='referenciasuc'),
nombrefiscal=(select valor from facturacion_nue.configuracion where nombre='empresa'),
noexterior=(select valor from facturacion_nue.configuracion where nombre='noexterior'),
nointerior=(select valor from facturacion_nue.configuracion where nombre='nointerior'),
colonia=(select valor from facturacion_nue.configuracion where nombre='colonia'),
municipio=(select valor from facturacion_nue.configuracion where nombre='municipio'),
referenciadomicilio=(select valor from facturacion_nue.configuracion where nombre='referenciasuc') where idsucursal=1;

#importar almacenes
delete from db_services.tblalmacenes where idalmacen<>1;
insert into db_services.tblalmacenes
select id+1,numero,nombre,direccion,1 from facturacion_nue.almacenes;

#importar existencias
delete from db_services.tblalmacenesi;
insert into db_services.tblalmacenesi (idinventario,idalmacen,cantidad)
select a.id+1,al.id+1,
ifnull(if(inventariable,facturacion_nue.existencia(now(),al.numero,0,a.id),facturacion_nue.existenciakit(now(),al.numero,0,a.id)),0)
from facturacion_nue.articulos a, facturacion_nue.almacenes al;

#importar precios
insert into db_services.tblinventarioprecios (idinventario,precio,comentario,idmoneda,idlista,utilidad)
select a.id+1,precio,'',2,lp.id,porcentaje from facturacion_nue.preciosarticulos
inner join facturacion_nue.listasprecios lp on lp.numero=numerolista inner join facturacion_nue.articulos a on a.codigo=codigoarticulo and lp.id<=5;

#importar notas de cargo ventas
insert into db_services.tblnotasdecargo (idcliente,fecha,folio,total,hora,estado,iva,totalapagar,idsucursal,
serie,tipodecambio,noaprobacion,yearaprobacion,nocertificado,eselectronica,idmoneda,aplicado,
fechacancelado,horacancelado,isr,ivaretenido,idconcepto) select c.id,concat(year(fecha),'/',lpad(month(fecha),2,'0'),'/',lpad(day(fecha),2,'0')),f.nofactura,
(select sum(cantidad*preciounitario-(cantidad*preciounitario*descuento*.01)) from facturacion_nue.articulosfactura af where  af.tipo=f.tipo and af.serie=f.serie and af.nofactura=f.nofactura) total,
concat(lpad(hour(fecha),2,'0'),':',lpad(minute(fecha),2,'0'),':',lpad(second(fecha),2,'0')),if(cancelada,4,3),
(select sum(cantidad*preciounitario-(cantidad*preciounitario*descuento*.01)+
((cantidad*preciounitario*descuento*.01))*iva*.01) from facturacion_nue.articulosfactura af where af.tipo=f.tipo and af.serie=f.serie and af.nofactura=f.nofactura) iva,
facturacion_nue.importefactura(f.tipo,f.serie,f.nofactura),1,f.serie,tipocambio,noaprobacion,anoaprobacion,nocertificado,
f.tipo=43 or f.tipo=45,if(fv.moneda=0,2,3),1,
if(isnull(fechacancelacion),'',concat(lpad(year(fechacancelacion),4,'0'),'/',lpad(month (fechacancelacion),2,'0'),'/',lpad(day   (fechacancelacion),2,'0'))),
if(isnull(fechacancelacion),'',concat(lpad(hour(fechacancelacion),2,'0'),':',lpad(minute(fechacancelacion),2,'0'),':',lpad(second(fechacancelacion),2,'0'))),0,0,1
from facturacion_nue.facturas f inner join facturacion_nue.facturasventas fv on f.tipo=fv.tipo and f.serie=fv.serie and f.nofactura=fv.nofactura
inner join facturacion_nue.clientes c on c.numero=fv.nocliente
where f.tipo=10 or f.tipo=43 or f.tipo=44 or f.tipo=45;

insert into db_services.tblnotasdecargodetalles (iddetalle,idinventario,idmoneda,
idcargo,cantidad,precio,descripcion,iva,extra,descuento)
select id,idarticulo+1,if(moneda=0,2,3),
(select idcargo from db_services.tblnotasdecargo nc where nc.serie=af.SERIE AND nc.folio=af.nofactura),
cantidad,preciounitario,descripcion,iva,0,descuento from facturacion_nue.articulosfactura af
inner join facturacion_nue.facturasventas fv on af.tipo=fv.tipo and af.serie=fv.serie and af.nofactura=fv.nofactura
where (af.tipo=10 or af.tipo=43 or af.tipo=44 or af.tipo=45) and not isnull(idarticulo);

insert into db_services.tblnotasdecargotimbrado (idcargo,uuid,fechatimbrado,sellocfd,nocertificadosat,sellosat) select (select idcargo from db_services.tblnotasdecargo nc where nc.serie=tf.SERIE AND nc.folio=tf.nofactura),
uuid,fechatimbrado,sellocfd,nocertificadosat,sellosat from facturacion_nue.timbresfiscales tf
where tf.tipo=10 or tf.tipo=43 or tf.tipo=44 or tf.tipo=45;

#importar notas de cargo compras
insert into db_services.tblnotasdecargocompras (idproveedor,fecha,folio,total,hora,estado,iva,totalapagar,idsucursal,tipodecambio,idmoneda,aplicado,
fechacancelado,horacancelado,idconcepto) select p.id,concat(year(fecha),'/',lpad(month(fecha),2,'0'),'/',lpad(day(fecha),2,'0')),f.nofactura,
(select sum(cantidad*preciounitario-(cantidad*preciounitario*descuento*.01)) from facturacion_nue.articulosfactura af where  af.tipo=f.tipo and af.serie=f.serie and af.nofactura=f.nofactura) total,
concat(lpad(hour(fecha),2,'0'),':',lpad(minute(fecha),2,'0'),':',lpad(second(fecha),2,'0')),if(cancelada,4,3),
(select sum(cantidad*preciounitario-(cantidad*preciounitario*descuento*.01)+
((cantidad*preciounitario*descuento*.01))*iva*.01) from facturacion_nue.articulosfactura af where af.tipo=f.tipo and af.serie=f.serie and af.nofactura=f.nofactura) iva,
facturacion_nue.importefactura(f.tipo,f.serie,f.nofactura),1,tipocambio,2,1,
if(isnull(fechacancelacion),'',concat(lpad(year(fechacancelacion),4,'0'),'/',lpad(month (fechacancelacion),2,'0'),'/',lpad(day   (fechacancelacion),2,'0'))),
if(isnull(fechacancelacion),'',concat(lpad(hour(fechacancelacion),2,'0'),':',lpad(minute(fechacancelacion),2,'0'),':',lpad(second(fechacancelacion),2,'0'))),1
from facturacion_nue.facturas f inner join facturacion_nue.facturascompras fc on f.tipo=fc.tipo and f.serie=fc.serie and f.nofactura=fc.nofactura
inner join facturacion_nue.proveedores p on p.numero=fc.noproveedor
where f.tipo=11;

insert into db_services.tblnotasdecargodetallesc (iddetalle,idinventario,idmoneda,
idcargo,cantidad,precio,descripcion,iva,extra,descuento)
select id,idarticulo+1,moneda,
(select idcargo from db_services.tblnotasdecargo nc where nc.serie=af.SERIE AND nc.folio=af.nofactura),
cantidad,preciounitario,descripcion,iva,0,descuento from facturacion_nue.articulosfactura af
inner join facturacion_nue.facturasventas fv on af.tipo=fv.tipo and af.serie=fv.serie and af.nofactura=fv.nofactura
where af.tipo=11 and not isnull(idarticulo);

#importar notas de credito ventas
insert into db_services.tblnotasdecredito (idcliente,fecha,folio,total,hora,estado,iva,totalapagar,idsucursal,
serie,tipodecambio,noaprobacion,yearaprobacion,nocertificado,eselectronica,idmoneda,aplicado,
fechacancelado,horacancelado,idconcepto,desglosar) select c.id,concat(year(fecha),'/',lpad(month(fecha),2,'0'),'/',lpad(day(fecha),2,'0')),f.nofactura,
(select sum(cantidad*preciounitario-(cantidad*preciounitario*descuento*.01)) from facturacion_nue.articulosfactura af where  af.tipo=f.tipo and af.serie=f.serie and af.nofactura=f.nofactura) total,
concat(lpad(hour(fecha),2,'0'),':',lpad(minute(fecha),2,'0'),':',lpad(second(fecha),2,'0')),if(cancelada,4,3),
(select sum(cantidad*preciounitario-(cantidad*preciounitario*descuento*.01)+
((cantidad*preciounitario*descuento*.01))*iva*.01) from facturacion_nue.articulosfactura af where af.tipo=f.tipo and af.serie=f.serie and af.nofactura=f.nofactura) iva,
facturacion_nue.importefactura(f.tipo,f.serie,f.nofactura),1,f.serie,tipocambio,noaprobacion,anoaprobacion,nocertificado,
f.tipo=29 or f.tipo=41,if(fv.moneda=0,2,3),1,
if(isnull(fechacancelacion),'',concat(lpad(year(fechacancelacion),4,'0'),'/',lpad(month (fechacancelacion),2,'0'),'/',lpad(day   (fechacancelacion),2,'0'))),
if(isnull(fechacancelacion),'',concat(lpad(hour(fechacancelacion),2,'0'),':',lpad(minute(fechacancelacion),2,'0'),':',lpad(second(fechacancelacion),2,'0'))),1,c.desglosaiva
from facturacion_nue.facturas f inner join facturacion_nue.facturasventas fv on f.tipo=fv.tipo and f.serie=fv.serie and f.nofactura=fv.nofactura
inner join facturacion_nue.clientes c on c.numero=fv.nocliente
where f.tipo=21 or f.tipo=29 or f.tipo=37 or f.tipo=41;

insert into db_services.tblnotasdecreditodetalles (iddetalle,idinventario,idmoneda,
idnota,cantidad,precio,descripcion,iva,extra,descuento,idventa)
select id,idarticulo+1,if(moneda=0,2,3),
(select idnota from db_services.tblnotasdecredito nc where nc.serie=af.SERIE AND nc.folio=af.nofactura),
cantidad,preciounitario,descripcion,iva,0,descuento,0 from facturacion_nue.articulosfactura af
inner join facturacion_nue.facturasventas fv on af.tipo=fv.tipo and af.serie=fv.serie and af.nofactura=fv.nofactura
where (af.tipo=21 or af.tipo=29 or af.tipo=37 or af.tipo=41) and not isnull(idarticulo);

insert into db_services.tblnotasdecreditotimbrado (idnota,uuid,fechatimbrado,sellocfd,nocertificadosat,sellosat)
select (select idnota from db_services.tblnotasdecredito nc where nc.serie=tf.SERIE AND nc.folio=tf.nofactura),
uuid,fechatimbrado,sellocfd,nocertificadosat,sellosat from facturacion_nue.timbresfiscales tf
where tf.tipo=21 or tf.tipo=29 or tf.tipo=37 or tf.tipo=41;

#importar notas de credito compras
delete from db_services.tblnotasdecreditocompras;
insert into db_services.tblnotasdecreditocompras (idproveedor,fecha,folio,total,hora,estado,iva,totalapagar,idsucursal,
tipodecambio,idmoneda,aplicado,fechacancelado,horacancelado,idconcepto)
select p.id,concat(year(fecha),'/',lpad(month(fecha),2,'0'),'/',lpad(day(fecha),2,'0')),f.nofactura,
(select sum(cantidad*preciounitario-(cantidad*preciounitario*descuento*.01)) from facturacion_nue.articulosfactura af where  af.tipo=f.tipo and af.serie=f.serie and af.nofactura=f.nofactura) total,
concat(lpad(hour(fecha),2,'0'),':',lpad(minute(fecha),2,'0'),':',lpad(second(fecha),2,'0')),if(cancelada,4,3),
(select sum(cantidad*preciounitario-(cantidad*preciounitario*descuento*.01)+
((cantidad*preciounitario*descuento*.01))*iva*.01) from facturacion_nue.articulosfactura af where af.tipo=f.tipo and af.serie=f.serie and af.nofactura=f.nofactura) iva,
facturacion_nue.importefactura(f.tipo,f.serie,f.nofactura),1,tipocambio,2,1,
if(isnull(fechacancelacion),'',concat(lpad(year(fechacancelacion),4,'0'),'/',lpad(month (fechacancelacion),2,'0'),'/',lpad(day   (fechacancelacion),2,'0'))),
if(isnull(fechacancelacion),'',concat(lpad(hour(fechacancelacion),2,'0'),':',lpad(minute(fechacancelacion),2,'0'),':',lpad(second(fechacancelacion),2,'0'))),1
from facturacion_nue.facturas f inner join facturacion_nue.facturascompras fc on f.tipo=fc.tipo and f.serie=fc.serie and f.nofactura=fc.nofactura
inner join facturacion_nue.proveedores p on p.numero=fc.noproveedor
where f.tipo=22;

insert into db_services.tblnotasdecreditodetallesc (iddetalle,idinventario,idmoneda,
idnota,cantidad,precio,descripcion,iva,extra,descuento,idcompra)
select id,idarticulo+1,if(moneda=0,2,3),
(select idnota from db_services.tblnotasdecreditocompras nc where nc.folio=af.nofactura),
cantidad,preciounitario,descripcion,iva,0,descuento,0 from facturacion_nue.articulosfactura af
inner join facturacion_nue.facturasventas fv on af.tipo=fv.tipo and af.serie=fv.serie and af.nofactura=fv.nofactura
where (af.tipo=22) and not isnull(idarticulo);

#importar cotizaciones
insert into db_services.tblventascotizaciones (idcliente,fecha,folio,total,hora,estado,iva,totalapagar,idsucursal,
serie,desglosar) select c.id,concat(year(fecha),'/',lpad(month(fecha),2,'0'),'/',lpad(day(fecha),2,'0')),f.nofactura,
(select sum(cantidad*preciounitario-(cantidad*preciounitario*descuento*.01)) from facturacion_nue.articulosfactura af where  af.tipo=f.tipo and af.serie=f.serie and af.nofactura=f.nofactura) total,
concat(lpad(hour(fecha),2,'0'),':',lpad(minute(fecha),2,'0'),':',lpad(second(fecha),2,'0')),if(cancelada,4,3),
(select sum(cantidad*preciounitario-(cantidad*preciounitario*descuento*.01)+
((cantidad*preciounitario*descuento*.01))*iva*.01) from facturacion_nue.articulosfactura af where af.tipo=f.tipo and af.serie=f.serie and af.nofactura=f.nofactura) iva,
facturacion_nue.importefactura(f.tipo,f.serie,f.nofactura),1,f.serie,c.desglosaiva
from facturacion_nue.facturas f inner join facturacion_nue.facturasventas fv on f.tipo=fv.tipo and f.serie=fv.serie and f.nofactura=fv.nofactura
inner join facturacion_nue.clientes c on c.numero=fv.nocliente
where f.tipo=4;

insert into db_services.tblventascotizacionesinventario (iddetalle,idinventario,idmoneda,
idcotizacion,cantidad,precio,descripcion,iva,extra,descuento,idvariante)
select id,idarticulo+1,if(moneda=0,2,3),
(select idcotizacion from db_services.tblventascotizaciones nc where nc.serie=af.SERIE AND nc.folio=af.nofactura),
cantidad,preciounitario,descripcion,iva,0,descuento,1 from facturacion_nue.articulosfactura af
inner join facturacion_nue.facturasventas fv on af.tipo=fv.tipo and af.serie=fv.serie and af.nofactura=fv.nofactura
where af.tipo=4 and not isnull(idarticulo);

#importar cotizaciones compras
insert into db_services.tblcomprascotizacionesb (idproveedor,fecha,folio,total,hora,estado,iva,totalapagar,idsucursal,
serie,idmoneda) select p.id,concat(year(fecha),'/',lpad(month(fecha),2,'0'),'/',lpad(day(fecha),2,'0')),f.nofactura,
(select sum(cantidad*preciounitario-(cantidad*preciounitario*descuento*.01)) from facturacion_nue.articulosfactura af where  af.tipo=f.tipo and af.serie=f.serie and af.nofactura=f.nofactura) total,
concat(lpad(hour(fecha),2,'0'),':',lpad(minute(fecha),2,'0'),':',lpad(second(fecha),2,'0')),if(cancelada,4,3),
(select sum(cantidad*preciounitario-(cantidad*preciounitario*descuento*.01)+
((cantidad*preciounitario*descuento*.01))*iva*.01) from facturacion_nue.articulosfactura af where af.tipo=f.tipo and af.serie=f.serie and af.nofactura=f.nofactura) iva,
facturacion_nue.importefactura(f.tipo,f.serie,f.nofactura),1,f.serie,2
from facturacion_nue.facturas f inner join facturacion_nue.facturascompras fc on f.tipo=fc.tipo and f.serie=fc.serie and f.nofactura=fc.nofactura
inner join facturacion_nue.proveedores p on p.numero=fc.noproveedor
where f.tipo=0;

insert into db_services.tblcomprascotizacionesdetalles (iddetalle,idinventario,idmoneda,
idcotizacion,cantidad,precio,descripcion,iva,extra,descuento)
select id,idarticulo+1,if(moneda=0,2,3),
(select idcotizacion from db_services.tblcomprascotizacionesb nc where nc.serie=af.SERIE AND nc.folio=af.nofactura),
cantidad,preciounitario,descripcion,iva,0,descuento from facturacion_nue.articulosfactura af
inner join facturacion_nue.facturasventas fv on af.tipo=fv.tipo and af.serie=fv.serie and af.nofactura=fv.nofactura
where af.tipo=0 and not isnull(idarticulo);

#importar pedidos
insert into db_services.tblventaspedidos (idcliente,fecha,folio,total,hora,estado,iva,totalapagar,idsucursal,
serie,desglosar) select c.id,concat(year(fecha),'/',lpad(month(fecha),2,'0'),'/',lpad(day(fecha),2,'0')),f.nofactura,
(select sum(cantidad*preciounitario-(cantidad*preciounitario*descuento*.01)) from facturacion_nue.articulosfactura af where  af.tipo=f.tipo and af.serie=f.serie and af.nofactura=f.nofactura) total,
concat(lpad(hour(fecha),2,'0'),':',lpad(minute(fecha),2,'0'),':',lpad(second(fecha),2,'0')),if(cancelada,4,3),
(select sum(cantidad*preciounitario-(cantidad*preciounitario*descuento*.01)+
((cantidad*preciounitario*descuento*.01))*iva*.01) from facturacion_nue.articulosfactura af where af.tipo=f.tipo and af.serie=f.serie and af.nofactura=f.nofactura) iva,
facturacion_nue.importefactura(f.tipo,f.serie,f.nofactura),1,f.serie,c.desglosaiva
from facturacion_nue.facturas f inner join facturacion_nue.facturasventas fv on f.tipo=fv.tipo and f.serie=fv.serie and f.nofactura=fv.nofactura
inner join facturacion_nue.clientes c on c.numero=fv.nocliente
where f.tipo=5;

insert into db_services.tblventaspedidosinventario (iddetalle,idinventario,idmoneda,
idpedido,cantidad,precio,descripcion,iva,extra,descuento,idvariante,surtido)
select id,idarticulo+1,if(moneda=0,2,3),
(select idpedido from db_services.tblventaspedidos nc where nc.serie=af.SERIE AND nc.folio=af.nofactura),
cantidad,preciounitario,descripcion,iva,0,descuento,1,1 from facturacion_nue.articulosfactura af
inner join facturacion_nue.facturasventas fv on af.tipo=fv.tipo and af.serie=fv.serie and af.nofactura=fv.nofactura
where (af.tipo=5) and not isnull(idarticulo);

#importar pedidos compras
insert into db_services.tblcompraspedidos (idproveedor,fecha,folio,total,hora,estado,iva,totalapagar,idsucursal,
serie,idmoneda) select p.id,concat(year(fecha),'/',lpad(month(fecha),2,'0'),'/',lpad(day(fecha),2,'0')),f.nofactura,
(select sum(cantidad*preciounitario-(cantidad*preciounitario*descuento*.01)) from facturacion_nue.articulosfactura af where  af.tipo=f.tipo and af.serie=f.serie and af.nofactura=f.nofactura) total,
concat(lpad(hour(fecha),2,'0'),':',lpad(minute(fecha),2,'0'),':',lpad(second(fecha),2,'0')),if(cancelada,4,3),
(select sum(cantidad*preciounitario-(cantidad*preciounitario*descuento*.01)+
((cantidad*preciounitario*descuento*.01))*iva*.01) from facturacion_nue.articulosfactura af where af.tipo=f.tipo and af.serie=f.serie and af.nofactura=f.nofactura) iva,
facturacion_nue.importefactura(f.tipo,f.serie,f.nofactura),1,f.serie,2
from facturacion_nue.facturas f inner join facturacion_nue.facturascompras fc on f.tipo=fc.tipo and f.serie=fc.serie and f.nofactura=fc.nofactura
inner join facturacion_nue.proveedores p on p.numero=fc.noproveedor
where f.tipo=5;

insert into db_services.tblcompraspedidosdetalles (iddetalle,idinventario,idmoneda,
idpedido,cantidad,precio,descripcion,iva,extra,descuento,surtido)
select id,idarticulo+1,if(moneda=0,2,3),
(select idpedido from db_services.tblcompraspedidos nc where nc.serie=af.SERIE AND nc.folio=af.nofactura),
cantidad,preciounitario,descripcion,iva,0,descuento,1 from facturacion_nue.articulosfactura af
inner join facturacion_nue.facturasventas fv on af.tipo=fv.tipo and af.serie=fv.serie and af.nofactura=fv.nofactura
where (af.tipo=1) and not isnull(idarticulo);

#importar remisiones
insert into db_services.tblventasremisiones (idcliente,fecha,folio,total,hora,estado,
iva,totalapagar,idsucursal,
serie,desglosar,credito,tipodecambio,idmoneda,usado,fechacancelado,horacancelado)
select c.id,concat(year(fecha),'/',lpad(month(fecha),2,'0'),'/',lpad(day(fecha),2,'0')),f.nofactura,
(select sum(cantidad*preciounitario-(cantidad*preciounitario*descuento*.01)) from facturacion_nue.articulosfactura af where  af.tipo=f.tipo and af.serie=f.serie and af.nofactura=f.nofactura) total,
concat(lpad(hour(fecha),2,'0'),':',lpad(minute(fecha),2,'0'),':',lpad(second(fecha),2,'0')),if(cancelada,4,3),
(select sum((cantidad*preciounitario*descuento*.01)+
(cantidad*preciounitario-(cantidad*preciounitario*descuento*.01))*iva*.01) from facturacion_nue.articulosfactura af where af.tipo=f.tipo and af.serie=f.serie and af.nofactura=f.nofactura) iva,
facturacion_nue.importefactura(f.tipo,f.serie,f.nofactura),1,f.serie,c.desglosaiva,
0,tipocambio,if(fv.moneda=0,2,3),cancelada,
if(isnull(fechacancelacion),'',concat(lpad(year(fechacancelacion),4,'0'),'/',lpad(month (fechacancelacion),2,'0'),'/',lpad(day   (fechacancelacion),2,'0'))),
if(isnull(fechacancelacion),'',concat(lpad(hour(fechacancelacion),2,'0'),':',lpad(minute(fechacancelacion),2,'0'),':',lpad(second(fechacancelacion),2,'0')))
from facturacion_nue.facturas f inner join facturacion_nue.facturasventas fv on f.tipo=fv.tipo and f.serie=fv.serie and f.nofactura=fv.nofactura
inner join facturacion_nue.clientes c on c.numero=fv.nocliente
where f.tipo=6;

insert into db_services.tblventasremisionesinventario (iddetalle,idinventario,idmoneda,
idremision,cantidad,precio,descripcion,iva,extra,descuento,idvariante,surtido,idalmacen,idservicio)
select af.id,idarticulo+1,if(moneda=0,2,3),
(select idremision from db_services.tblventasremisiones nc where nc.serie=af.SERIE AND nc.folio=af.nofactura),
cantidad,preciounitario,descripcion,af.iva,0,descuento,1,1,a.id+1,0 from facturacion_nue.articulosfactura af
inner join facturacion_nue.facturasventas fv on af.tipo=fv.tipo and af.serie=fv.serie and af.nofactura=fv.nofactura
inner join facturacion_nue.facturas f on f.tipo=fv.tipo and f.serie=fv.serie and f.nofactura=fv.nofactura
inner join facturacion_nue.almacenes a on a.numero=f.noalmacen
where (af.tipo=6) and not isnull(idarticulo);

#importar remisiones compras
insert into db_services.tblcomprasremisiones (idproveedor,fecha,folio,total,hora,estado,iva,totalapagar,idsucursal,
serie,tipodecambio,idmoneda,usado,fechacancelado,horacancelado,folioi)
select p.id,concat(year(fecha),'/',lpad(month(fecha),2,'0'),'/',lpad(day(fecha),2,'0')),fc.folio,
(select sum(cantidad*preciounitario-(cantidad*preciounitario*descuento*.01)) from facturacion_nue.articulosfactura af where  af.tipo=f.tipo and af.serie=f.serie and af.nofactura=f.nofactura) total,
concat(lpad(hour(fecha),2,'0'),':',lpad(minute(fecha),2,'0'),':',lpad(second(fecha),2,'0')),if(cancelada,4,3),
(select sum((cantidad*preciounitario*descuento*.01)+
(cantidad*preciounitario-(cantidad*preciounitario*descuento*.01))*iva*.01) from facturacion_nue.articulosfactura af where af.tipo=f.tipo and af.serie=f.serie and af.nofactura=f.nofactura) iva,
facturacion_nue.importefactura(f.tipo,f.serie,f.nofactura),1,f.serie,tipocambio,2,cancelada,
if(isnull(fechacancelacion),'',concat(lpad(year(fechacancelacion),4,'0'),'/',lpad(month (fechacancelacion),2,'0'),'/',lpad(day   (fechacancelacion),2,'0'))),
if(isnull(fechacancelacion),'',concat(lpad(hour(fechacancelacion),2,'0'),':',lpad(minute(fechacancelacion),2,'0'),':',lpad(second(fechacancelacion),2,'0'))),f.nofactura
from facturacion_nue.facturas f inner join facturacion_nue.facturascompras fc on f.tipo=fc.tipo and f.serie=fc.serie and f.nofactura=fc.nofactura
inner join facturacion_nue.proveedores p on p.numero=fc.noproveedor
where f.tipo=2;

insert into db_services.tblcomprasremisionesdetalles (iddetalle,idinventario,idmoneda,
idremision,cantidad,precio,descripcion,iva,extra,descuento,surtido,idalmacen)
select af.id,idarticulo+1,if(moneda=0,2,3),
(select idremision from db_services.tblcomprasremisiones nc where nc.serie=af.SERIE AND nc.folio=af.nofactura),
cantidad,preciounitario,descripcion,af.iva,0,descuento,1,a.id+1 from facturacion_nue.articulosfactura af
inner join facturacion_nue.facturasventas fv on af.tipo=fv.tipo and af.serie=fv.serie and af.nofactura=fv.nofactura
inner join facturacion_nue.facturas f on f.tipo=fv.tipo and f.serie=fv.serie and f.nofactura=fv.nofactura
inner join facturacion_nue.almacenes a on a.numero=f.noalmacen
where (af.tipo=2) and not isnull(idarticulo);

#importar vendedores
delete from db_services.tblvendedores where idvendedor<>1;
insert into db_services.tblvendedores select id,nombre,'','','',numero,'','','',''
from facturacion_nue.vendedores where id<>1;

#importar facturas
insert into db_services.tblventas (idcliente,fecha,folio,total,hora,estado,iva,totalapagar,idsucursal,
serie,desglosar,credito,tipodecambio,idconversion,facturado,fechacancelado,horacancelado,nocertificado,
noaprobacion,yearaprobacion,eselectronica,idforma,ivaretenido,isr,idvendedor,comentariof,nocuenta)
select c.id,
concat(year(f.fecha),'/',lpad(month(f.fecha),2,'0'),'/',lpad(day(f.fecha),2,'0')),
f.nofactura,
(select sum(cantidad*preciounitario-(cantidad*preciounitario*descuento*.01)) from facturacion_nue.articulosfactura af where  af.tipo=f.tipo and af.serie=f.serie and af.nofactura=f.nofactura) total,
concat(lpad(hour(f.fecha),2,'0'),':',lpad(minute(f.fecha),2,'0'),':',lpad(second(f.fecha),2,'0')),
if(cancelada,4,3),
(select sum((cantidad*preciounitario*descuento*.01)+(cantidad*preciounitario-(cantidad*preciounitario*descuento*.01))*iva*.01) from facturacion_nue.articulosfactura af where af.tipo=f.tipo and af.serie=f.serie and af.nofactura=f.nofactura) iva,
facturacion_nue.importefactura(f.tipo,f.serie,f.nofactura),
1,
f.serie,
0,
0 credito,
f.tipocambio,if(fv.moneda=0,2,3),
0,
if(isnull(f.fechacancelacion),'',concat(lpad(year(f.fechacancelacion),4,'0'),'/',lpad(month (f.fechacancelacion),2,'0'),'/',lpad(day   (f.fechacancelacion),2,'0'))),
if(isnull(f.fechacancelacion),'',concat(lpad(hour(f.fechacancelacion),2,'0'),':',lpad(minute(f.fechacancelacion),2,'0'),':',lpad(second(f.fechacancelacion),2,'0'))),
nocertificado,noaprobacion,anoaprobacion,f.tipo=27 or f.tipo=39,if(facturacion_nue.tipoventafactura(f.tipo,f.serie,f.nofactura)=1,1,2),0,0,v.id,comentario,''
from facturacion_nue.facturas f
inner join facturacion_nue.facturasventas fv on f.tipo=fv.tipo and f.serie=fv.serie and f.nofactura=fv.nofactura
inner join facturacion_nue.clientes c on c.numero=fv.nocliente
inner join facturacion_nue.vendedores v on v.numero=fv.novendedor
where (f.tipo=7 or f.tipo=27 or f.tipo=35 or f.tipo=39);

insert into db_services.tblventasinventario (idventasinventario,idinventario,idmoneda,
idventa,cantidad,precio,descripcion,iva,extra,descuento,idvariante,surtido,idalmacen,idservicio,cantidadm,tipocantidadm)
select af.id,idarticulo+1,if(moneda=0,2,3),
(select idventa from db_services.tblventas nc where nc.serie=af.SERIE AND nc.folio=af.nofactura),
cantidad,preciounitario,descripcion,af.iva,0,descuento,1,1,a.id+1,0,cantidad,7 from facturacion_nue.articulosfactura af
inner join facturacion_nue.facturasventas fv on af.tipo=fv.tipo and af.serie=fv.serie and af.nofactura=fv.nofactura
inner join facturacion_nue.facturas f on f.tipo=fv.tipo and f.serie=fv.serie and f.nofactura=fv.nofactura
inner join facturacion_nue.almacenes a on a.numero=f.noalmacen
where (f.tipo=7 or f.tipo=27 or f.tipo=35 or f.tipo=39) and not isnull(idarticulo);

insert into db_services.tblventastimbrado (idventa,uuid,fechatimbrado,sellocfd,nocertificadosat,sellosat)
select (select idventa from db_services.tblventas nc where nc.serie=tf.SERIE AND nc.folio=tf.nofactura),
uuid,fechatimbrado,sellocfd,nocertificadosat,sellosat from facturacion_nue.timbresfiscales tf
where tf.tipo=7 or tf.tipo=27 or tf.tipo=35 or tf.tipo=39;


#importar recibos
insert into db_services.tblventas (idcliente,fecha,folio,total,hora,estado,iva,totalapagar,idsucursal,
serie,desglosar,credito,tipodecambio,idconversion,facturado,fechacancelado,horacancelado,nocertificado,
noaprobacion,yearaprobacion,eselectronica,idforma,ivaretenido,isr,idvendedor,comentariof,nocuenta)
select c.id,
concat(year(f.fecha),'/',lpad(month(f.fecha),2,'0'),'/',lpad(day(f.fecha),2,'0')),
f.nofactura,
(select sum(cantidad*preciounitario-(cantidad*preciounitario*descuento*.01)) from facturacion_nue.articulosfactura af where  af.tipo=f.tipo and af.serie=f.serie and af.nofactura=f.nofactura) total,
concat(lpad(hour(f.fecha),2,'0'),':',lpad(minute(f.fecha),2,'0'),':',lpad(second(f.fecha),2,'0')),
if(cancelada,4,3),
(select sum((cantidad*preciounitario*descuento*.01)+(cantidad*preciounitario-(cantidad*preciounitario*descuento*.01))*iva*.01) from facturacion_nue.articulosfactura af where af.tipo=f.tipo and af.serie=f.serie and af.nofactura=f.nofactura) iva,
facturacion_nue.importefactura(f.tipo,f.serie,f.nofactura),
1,
f.serie,
0,
0 credito,
f.tipocambio,if(fv.moneda=0,2,3),
0,
if(isnull(f.fechacancelacion),'',concat(lpad(year(f.fechacancelacion),4,'0'),'/',lpad(month (f.fechacancelacion),2,'0'),'/',lpad(day   (f.fechacancelacion),2,'0'))),
if(isnull(f.fechacancelacion),'',concat(lpad(hour(f.fechacancelacion),2,'0'),':',lpad(minute(f.fechacancelacion),2,'0'),':',lpad(second(f.fechacancelacion),2,'0'))),
nocertificado,noaprobacion,anoaprobacion,1,if(facturacion_nue.tipoventafactura(f.tipo,f.serie,f.nofactura)=1,1,2),fv.pretencioniva,fv.pretencionisr,v.id,comentario,''
from facturacion_nue.facturas f
inner join facturacion_nue.facturasventas fv on f.tipo=fv.tipo and f.serie=fv.serie and f.nofactura=fv.nofactura
inner join facturacion_nue.clientes c on c.numero=fv.nocliente
inner join facturacion_nue.vendedores v on v.numero=fv.novendedor
where f.tipo=31 or f.tipo=42;

insert into db_services.tblventasinventario (idventasinventario,idinventario,idmoneda,
idventa,cantidad,precio,descripcion,iva,extra,descuento,idvariante,surtido,idalmacen,idservicio,cantidadm,tipocantidadm)
select af.id,idarticulo+1,if(moneda=0,2,3),
(select idventa from db_services.tblventas nc where nc.serie=af.SERIE AND nc.folio=af.nofactura),
cantidad,preciounitario,descripcion,af.iva,0,descuento,1,1,a.id+1,0,cantidad,7 from facturacion_nue.articulosfactura af
inner join facturacion_nue.facturasventas fv on af.tipo=fv.tipo and af.serie=fv.serie and af.nofactura=fv.nofactura
inner join facturacion_nue.facturas f on f.tipo=fv.tipo and f.serie=fv.serie and f.nofactura=fv.nofactura
inner join facturacion_nue.almacenes a on a.numero=f.noalmacen
where (f.tipo=31 or f.tipo=42) and not isnull(idarticulo);

insert into db_services.tblventastimbrado (idventa,uuid,fechatimbrado,sellocfd,nocertificadosat,sellosat)
select (select idventa from db_services.tblventas nc where nc.serie=tf.SERIE AND nc.folio=tf.nofactura),
uuid,fechatimbrado,sellocfd,nocertificadosat,sellosat from facturacion_nue.timbresfiscales tf
where tf.tipo=31 or tf.tipo=42;


#importar facturas compras
insert into db_services.tblcompras (idproveedor,fecha,referencia,total,hora,estado,totalapagar,idsucursal,
serie,desglosar,credito,tipodecambio,idmoneda,fechacancelado,horacancelado,idforma,costoindirecto,folioi)
select c.id,concat(year(f.fecha),'/',lpad(month(f.fecha),2,'0'),'/',lpad(day(f.fecha),2,'0')),fv.folio,
(select sum(cantidad*preciounitario-(cantidad*preciounitario*descuento*.01)) from facturacion_nue.articulosfactura af where  af.tipo=f.tipo and af.serie=f.serie and af.nofactura=f.nofactura) total,
concat(lpad(hour(f.fecha),2,'0'),':',lpad(minute(f.fecha),2,'0'),':',lpad(second(f.fecha),2,'0')),if(cancelada,4,3),
facturacion_nue.importefactura(f.tipo,f.serie,f.nofactura),1,f.serie,1,
0 credito,
f.tipocambio,2,
if(isnull(f.fechacancelacion),'',concat(lpad(year(f.fechacancelacion),4,'0'),'/',lpad(month (f.fechacancelacion),2,'0'),'/',lpad(day   (f.fechacancelacion),2,'0'))),
if(isnull(f.fechacancelacion),'',concat(lpad(hour(f.fechacancelacion),2,'0'),':',lpad(minute(f.fechacancelacion),2,'0'),':',lpad(second(f.fechacancelacion),2,'0'))),
if(facturacion_nue.tipoventafactura(f.tipo,f.serie,f.nofactura)=1,1,2),costoenvio,f.nofactura
from facturacion_nue.facturas f inner join facturacion_nue.facturascompras fv on f.tipo=fv.tipo and f.serie=fv.serie and f.nofactura=fv.nofactura
inner join facturacion_nue.proveedores c on c.numero=fv.noproveedor
where f.tipo=3;

insert into db_services.tblcomprasdetalles (iddetalle,idinventario,idmoneda,
idcompra,cantidad,precio,iva,extra,descuento,surtido,idalmacen,costoindirecto)
select af.id,idarticulo+1,2,
(select idcompra from db_services.tblcompras nc where nc.serie=af.SERIE AND nc.referencia=fv.folio),
cantidad,preciounitario,af.iva,0,descuento,1,a.id+1,facturacion_nue.costoenvio(af.id) from facturacion_nue.articulosfactura af
inner join facturacion_nue.facturascompras fv on af.tipo=fv.tipo and af.serie=fv.serie and af.nofactura=fv.nofactura
inner join facturacion_nue.facturas f on f.tipo=fv.tipo and f.serie=fv.serie and f.nofactura=fv.nofactura
inner join facturacion_nue.almacenes a on a.numero=f.noalmacen
where (f.tipo=3) and not isnull(idarticulo);

#importar pagos ventas
insert into db_services.tblventaspagos (cantidad,estado,idventa,fecha,tipo,iddocumento,tipodocumento,hora,
fechacancelado,horacancelado,idcargo,idcliente,idmoneda,ptipodecambio,iddocumentod,tipodoci,idconceptonotaventa)
select p.cantidad,if(cancelado,4,3),
(select idventa from db_services.tblventas nc where nc.serie=fp.SERIE AND nc.folio=fp.nofactura),
if(isnull(p.fecha),'',concat(lpad(year(p.fecha),4,'0'),'/',lpad(month (p.fecha),2,'0'),'/',lpad(day   (p.fecha),2,'0'))),'',0,0,
if(isnull(p.fecha),'',concat(lpad(hour(p.fecha),2,'0'),':',lpad(minute(p.fecha),2,'0'),':',lpad(second(p.fecha),2,'0'))),
if(isnull(p.fechacancelacion),'',concat(lpad(year(p.fechacancelacion),4,'0'),'/',lpad(month (p.fechacancelacion),2,'0'),'/',lpad(day   (p.fechacancelacion),2,'0'))),
if(isnull(p.fechacancelacion),'',concat(lpad(hour(p.fechacancelacion),2,'0'),':',lpad(minute(p.fechacancelacion),2,'0'),':',lpad(second(p.fechacancelacion),2,'0'))),
0,c.id,2,1,0,0,3 from facturacion_nue.pagos p
inner join facturacion_nue.pagosclientes pc on p.tipo=pc.tipo and p.folio=pc.folio
inner join facturacion_nue.clientes c on c.numero=pc.nocliente
inner join facturacion_nue.facturaspagos fp on fp.tipo=p.tipo and fp.folio=p.folio
where p.tipo=17 and (fp.tipofactura=7 or fp.tipofactura=27 or fp.tipofactura=35 or fp.tipofactura=39 or fp.tipofactura=31 or fp.tipofactura=42);

#importar pagos compras
insert into db_services.tblcompraspagos (cantidad,estado,idcompra,fecha,tipo,iddocumento,tipodocumento,hora,
fechacancelado,horacancelado,idcargo,idproveedor,idmoneda,ptipodecambio,iddocumentod,tipodoci,idconceptonotacompra)
select p.cantidad,if(cancelado,4,3),
(select idcompra from db_services.tblcompras nc where nc.serie=fp.SERIE AND nc.referencia=fv.folio),
if(isnull(p.fecha),'',concat(lpad(year(p.fecha),4,'0'),'/',lpad(month (p.fecha),2,'0'),'/',lpad(day   (p.fecha),2,'0'))),'',0,0,
if(isnull(p.fecha),'',concat(lpad(hour(p.fecha),2,'0'),':',lpad(minute(p.fecha),2,'0'),':',lpad(second(p.fecha),2,'0'))),
if(isnull(p.fechacancelacion),'',concat(lpad(year(p.fechacancelacion),4,'0'),'/',lpad(month (p.fechacancelacion),2,'0'),'/',lpad(day   (p.fechacancelacion),2,'0'))),
if(isnull(p.fechacancelacion),'',concat(lpad(hour(p.fechacancelacion),2,'0'),':',lpad(minute(p.fechacancelacion),2,'0'),':',lpad(second(p.fechacancelacion),2,'0'))),
0,c.id,2,1,0,0,3 from facturacion_nue.pagos p
inner join facturacion_nue.pagosproveedores pc on p.tipo=pc.tipo and p.folio=pc.folio
inner join facturacion_nue.proveedores c on c.numero=pc.noproveedor
inner join facturacion_nue.facturaspagos fp on fp.tipo=p.tipo and fp.folio=p.folio
inner join facturacion_nue.facturascompras fv on fp.tipofactura=fv.tipo and fp.serie=fv.serie and fp.nofactura=fv.nofactura
where p.tipo=18 and (fp.tipofactura=3);

#importar movimientos
insert into db_services.tblmovimientos (fecha,folio,total,hora,estado,totalapagar,idsucursal,
serie,tipodecambio,fechacancelado,horacancelado,idconcepto,comentario,idmoneda)
select concat(year(fecha),'/',lpad(month(fecha),2,'0'),'/',lpad(day(fecha),2,'0')),f.nofactura,
(select sum(cantidad*preciounitario-(cantidad*preciounitario*descuento*.01)) from facturacion_nue.articulosfactura af where  af.tipo=f.tipo and af.serie=f.serie and af.nofactura=f.nofactura) total,
concat(lpad(hour(fecha),2,'0'),':',lpad(minute(fecha),2,'0'),':',lpad(second(fecha),2,'0')),if(cancelada,4,3),
facturacion_nue.importefactura(f.tipo,f.serie,f.nofactura),1,f.serie,tipocambio,
if(isnull(fechacancelacion),'',concat(lpad(year(fechacancelacion),4,'0'),'/',lpad(month (fechacancelacion),2,'0'),'/',lpad(day   (fechacancelacion),2,'0'))),
if(isnull(fechacancelacion),'',concat(lpad(hour(fechacancelacion),2,'0'),':',lpad(minute(fechacancelacion),2,'0'),':',lpad(second(fechacancelacion),2,'0'))),
case f.tipo when 9 then 4 when 12 then 1 when 13 then 3 when 14 then 8 when 16 then 5 end,
comentario,2
from facturacion_nue.facturas f inner join facturacion_nue.movimientos fv on f.tipo=fv.tipo and f.serie=fv.serie and f.nofactura=fv.nofactura
where (f.tipo=9 or f.tipo=12 or f.tipo=13 or f.tipo=14 or f.tipo=15 or f.tipo=16) ;

insert into db_services.tblmovimientosdetalles (iddetalle,idinventario,idmoneda,
idmovimiento,cantidad,precio,descripcion,idvariante,surtido,idalmacen,idalmacen2,inventarioanterior)
select af.id,idarticulo+1,2,
(select idmovimiento from db_services.tblmovimientos nc where nc.serie=af.SERIE AND nc.folio=af.nofactura and idconcepto=
case af.tipo when 9 then 4 when 12 then 1 when 13 then 3 when 14 then 8 when 16 then 5 end),
if(f.tipo=16,facturacion_nue.existencia(now(),noalmacen,0,a.id)+cantidad,cantidad),preciounitario,descripcion,1,1,a.id+1,ifnull(a2.id+1,a.id+1),if(f.tipo=16,cantidad,0)
from facturacion_nue.articulosfactura af
inner join facturacion_nue.movimientos fv on af.tipo=fv.tipo and af.serie=fv.serie and af.nofactura=fv.nofactura
inner join facturacion_nue.facturas f on f.tipo=fv.tipo and f.serie=fv.serie and f.nofactura=fv.nofactura
inner join facturacion_nue.almacenes a on a.numero=f.noalmacen
left outer join facturacion_nue.almacenes a2 on a2.numero=fv.noalmacenb
where (f.tipo=9 or f.tipo=12 or f.tipo=13 or f.tipo=14 or f.tipo=15 or f.tipo=16) and not isnull(idarticulo);

#importar series
insert into db_services.tblinventarioseries (idinventario,noserie,fechacaducidad,
fechagarantia,idcompra,idventa,idremision,idservicio,idmovimiento,idremisionc,idcotizacion)
select idarticulo+1,numero,
concat(year(fecha),'/',lpad(month(fecha),2,'0'),'/',lpad(day(fecha),2,'0')),
concat(year(fecha),'/',lpad(month(fecha),2,'0'),'/',lpad(day(fecha),2,'0')),
0,0,0,0,0,0,0
from facturacion_nue.articulosfactura af inner join facturacion_nue.series s on af.id=s.idarticulofactura;

update db_services.tblinventarioseries
inner join db_services.tblventas v on true
inner join facturacion_nue.articulosfactura af on v.serie=af.serie and v.folio=af.nofactura
inner join facturacion_nue.series s on af.id=s.idarticulofactura
set db_services.tblinventarioseries.idventa=v.idventa
where db_services.tblinventarioseries.noserie=s.numero and
(af.tipo=7 or af.tipo=27 or af.tipo=35 or af.tipo=39);

update db_services.tblinventarioseries
inner join db_services.tblventasremisiones v on true
inner join facturacion_nue.articulosfactura af on v.serie=af.serie and v.folio=af.nofactura
inner join facturacion_nue.series s on af.id=s.idarticulofactura
set db_services.tblinventarioseries.idremision=v.idremision
where db_services.tblinventarioseries.noserie=s.numero and
(af.tipo=6);

update db_services.tblinventarioseries
inner join db_services.tblcompras v on true
inner join facturacion_nue.articulosfactura af on v.serie=af.serie and v.folioi=af.nofactura
inner join facturacion_nue.series s on af.id=s.idarticulofactura
set db_services.tblinventarioseries.idcompra=v.idcompra
where db_services.tblinventarioseries.noserie=s.numero and
(af.tipo=3);

update db_services.tblinventarioseries
inner join db_services.tblcomprasremisiones v on true
inner join facturacion_nue.articulosfactura af on v.serie=af.serie and v.folioi=af.nofactura
inner join facturacion_nue.series s on af.id=s.idarticulofactura
set db_services.tblinventarioseries.idremisionc=v.idremision
where db_services.tblinventarioseries.noserie=s.numero and
(af.tipo=2);

update db_services.tblinventarioseries
inner join db_services.tblmovimientos v on true
inner join facturacion_nue.articulosfactura af on v.serie=af.serie and v.folio=af.nofactura
inner join facturacion_nue.series s on af.id=s.idarticulofactura
set db_services.tblinventarioseries.idmovimiento=v.idmovimiento
where db_services.tblinventarioseries.noserie=s.numero and
(af.tipo=9 or af.tipo=12 or af.tipo=13 or af.tipo=14 or af.tipo=15 or af.tipo=16);

#da de alta los folios
alter table db_services.tblsucursalesfolios modify column yearaprobacion varchar(10) not null;

#importar folios
delete FROM db_services.tblsucursalesfolios;
insert into db_services.tblsucursalesfolios select id,1,inicial,final,serie,
iddocumento=27 or iddocumento=29 or iddocumento=30 or iddocumento=31 or iddocumento=39
or iddocumento=40 or iddocumento=41 or iddocumento=42 or iddocumento=43 or iddocumento=45,
1,noaprobacion,anoaprobacion,actual<=final, case iddocumento
when 0 then 8 when 1 then 9 when 4 then 6 when 5 then 7 when 6 then 4 when 7 then 1
when 10 then 5 when 19 then 3 when 21 then 2 when 27 then 1 when 29 then 2
when 30 then 3 when 35 then 1 when 36 then 3 when 37 then 2 when 39 then 1
when 40 then 3 when 41 then 2 when 43 then 5 when 44 then 5
when 45 then 5 end,'0000' from facturacion_nue.folios where (actual>1 or inicial>1
or final>0 or anoaprobacion<>'' or noaprobacion<>'' or avisarresten<>0) and (
iddocumento=0 or iddocumento=1 or iddocumento=4 or iddocumento=5 or iddocumento=6
 or iddocumento=7 or iddocumento=10 or iddocumento=19 or iddocumento=21 or iddocumento=27
  or iddocumento=29 or iddocumento=30 or iddocumento=35 or iddocumento=36 or iddocumento=37
   or iddocumento=39 or iddocumento=40 or iddocumento=41 or iddocumento=43 or iddocumento=44 or iddocumento=45);

update db_services.tblventas set credito=ifnull((select sum(cantidad) from db_services.tblventaspagos where idventa=db_services.tblventas.idventa),0);
update db_services.tblcompras set credito=ifnull((select sum(cantidad) from db_services.tblcompraspagos where idcompra=db_services.tblcompras.idcompra),0);

update db_services.tblventasinventario inner join db_services.tblventas on db_services.tblventasinventario.idventa=db_services.tblventas.idventa set precio=precio*cantidad;
update db_services.tblventasremisionesinventario inner join db_services.tblventasremisiones on db_services.tblventasremisionesinventario.idremision=db_services.tblventasremisiones.idremision set precio=precio*cantidad;
update db_services.tblventaspedidosinventario inner join db_services.tblventaspedidos on db_services.tblventaspedidosinventario.idpedido=db_services.tblventaspedidos.idpedido set precio=precio*cantidad;
update db_services.tblventascotizacionesinventario inner join db_services.tblventascotizaciones on db_services.tblventascotizacionesinventario.idcotizacion=db_services.tblventascotizaciones.idcotizacion set precio=precio*cantidad;
update db_services.tblnotasdecargodetalles inner join db_services.tblnotasdecargo on db_services.tblnotasdecargodetalles.idcargo=db_services.tblnotasdecargo.idcargo set precio=precio*cantidad;
update db_services.tblnotasdecreditodetalles inner join db_services.tblnotasdecredito on db_services.tblnotasdecreditodetalles.idnota=db_services.tblnotasdecredito.idnota set precio=precio*cantidad;
update db_services.tbldevolucionesdetalles inner join db_services.tbldevoluciones on db_services.tbldevolucionesdetalles.iddevolucion=db_services.tbldevoluciones.iddevolucion set precio=precio*cantidad;
update db_services.tblcomprasdetalles inner join db_services.tblcompras on db_services.tblcomprasdetalles.idcompra=db_services.tblcompras.idcompra set precio=precio*cantidad;
update db_services.tblcomprasremisionesdetalles inner join db_services.tblcomprasremisiones on db_services.tblcomprasremisionesdetalles.idremision=db_services.tblcomprasremisiones.idremision set precio=precio*cantidad;
update db_services.tblcompraspedidosdetalles inner join db_services.tblcompraspedidos on db_services.tblcompraspedidosdetalles.idpedido=db_services.tblcompraspedidos.idpedido set precio=precio*cantidad;
update db_services.tblcomprascotizacionesdetalles inner join db_services.tblcomprascotizacionesb on db_services.tblcomprascotizacionesdetalles.idcotizacion=db_services.tblcomprascotizacionesb.idcotizacion set precio=precio*cantidad;
update db_services.tbldevolucionesdetallesc inner join db_services.tbldevolucionescompras on db_services.tbldevolucionesdetallesc.iddevolucion=db_services.tbldevolucionescompras.iddevolucion set precio=precio*cantidad;
update db_services.tblnotasdecargodetallesc inner join db_services.tblnotasdecargocompras on db_services.tblnotasdecargodetallesc.idcargo=db_services.tblnotasdecargocompras.idcargo set precio=precio*cantidad;
update db_services.tblnotasdecreditodetallesc inner join db_services.tblnotasdecreditocompras on db_services.tblnotasdecreditodetallesc.idnota=db_services.tblnotasdecreditocompras.idnota set precio=precio*cantidad;

delete db_services.tblventaspagos from db_services.tblventaspagos,db_services.tblventas where db_services.tblventaspagos.idventa=db_services.tblventas.idventa and db_services.tblventas.idforma=1;
delete db_services.tblcompraspagos from db_services.tblcompraspagos,db_services.tblcompras where db_services.tblcompraspagos.idcompra=db_services.tblcompras.idcompra and db_services.tblcompras.idforma=1;


update db_services.tblventas set fechacancelado=fecha,horacancelado=hora where db_services.tblventas.estado=3;
update db_services.tblventasremisiones set fechacancelado=fecha,horacancelado=hora where db_services.tblventasremisiones.estado=3;
update db_services.tbldevoluciones set fechacancelado=fecha,horacancelado=hora where db_services.tbldevoluciones.estado=3;
update db_services.tblnotasdecargo set fechacancelado=fecha,horacancelado=hora where db_services.tblnotasdecargo.estado=3;
update db_services.tblnotasdecredito set fechacancelado=fecha,horacancelado=hora where db_services.tblnotasdecredito.estado=3;
update db_services.tblcompras set fechacancelado=fecha,horacancelado=hora where db_services.tblcompras.estado=3;
update db_services.tblcomprasremisiones set fechacancelado=fecha,horacancelado=hora where db_services.tblcomprasremisiones.estado=3;
update db_services.tbldevolucionescompras set fechacancelado=fecha,horacancelado=hora where db_services.tbldevolucionescompras.estado=3;
update db_services.tblnotasdecargocompras set fechacancelado=fecha,horacancelado=hora where db_services.tblnotasdecargocompras.estado=3;
update db_services.tblnotasdecreditocompras set fechacancelado=fecha,horacancelado=hora where db_services.tblnotasdecreditocompras.estado=3;

#cambiar la version 2.2 3.2
#update tblopciones set fechapunto2='2012/05/09'
