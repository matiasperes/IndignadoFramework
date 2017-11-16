USE [IndignadoFramework];
GO
----------------------------------------------------------------

select * from Movimientoset

insert Movimientoset (Nombre,ubicacionlatitud,ubicacionlongitud,descripcion,logo,estilo,m,n,x,z) values ('GreenPeace',	-33,	-56,	'Lucha por el medio ambiente.',	'http://2.bp.blogspot.com/--WMGnpAxNfc/T7M_QNyC5nI/AAAAAAAAAGQ/KWc5jW0oL7s/s1600/Greenpeace.png',	'Blue',	20,	20,	10,	10)
insert Movimientoset (Nombre,ubicacionlatitud,ubicacionlongitud,descripcion,logo,estilo,m,n,x,z) values ('CruzRoja',	-30,	-56,	'Movimiento humanitario mundial.',	'https://dl.dropbox.com/u/85924014/cruzroja.png',	'Red',	10,	10,	10,	10)

select * from fuentewebset

select * from convocatoriaset

select * from EspecificacionUsuarioSet

----------------------------------------------------------------

INSERT "CategoriaTematicaSet"("Nombre","Descripcion") VALUES('Cambio climatico','Descripcion')
INSERT "CategoriaTematicaSet"("Nombre","Descripcion") VALUES('Contaminacion del agua','Descripcion')
INSERT "CategoriaTematicaSet"("Nombre","Descripcion") VALUES('Aire y proteccion de la atmosfera','Descripcion')
INSERT "CategoriaTematicaSet"("Nombre","Descripcion") VALUES('Efecto invernadero','Descripcion')
INSERT "CategoriaTematicaSet"("Nombre","Descripcion") VALUES('Contaminacion del aire','Descripcion')
INSERT "CategoriaTematicaSet"("Nombre","Descripcion") VALUES('Deforestacion','Descripcion')
INSERT "CategoriaTematicaSet"("Nombre","Descripcion") VALUES('Reciclaje','Descripcion')
INSERT "CategoriaTematicaSet"("Nombre","Descripcion") VALUES('Especies en peligro de extincion','Descripcion')
INSERT "CategoriaTematicaSet"("Nombre","Descripcion") VALUES('Recursos naturales','Descripcion')
INSERT "CategoriaTematicaSet"("Nombre","Descripcion") VALUES('Plaguicidas','Descripcion')
INSERT "CategoriaTematicaSet"("Nombre","Descripcion") VALUES('Derechos Estudiantiles','Descripcion')
INSERT "CategoriaTematicaSet"("Nombre","Descripcion") VALUES('Sobrepesca','Descripcion')
INSERT "CategoriaTematicaSet"("Nombre","Descripcion") VALUES('Transgenicos','Descripción')
INSERT "CategoriaTematicaSet"("Nombre","Descripcion") VALUES('Derechos Humanos','Descripcion')
INSERT "CategoriaTematicaSet"("Nombre","Descripcion") VALUES('Otros','Descripción')

INSERT "ConvocatoriaSet"("Inicio","UbicacionLatitud","UbicacionLongitud","Descripcion","Quorum","Titulo","MovimientoId","CategoriaTematicaId","CantUsuariosConfirmados","Suspendida") VALUES('10/7/2012', -33, -56,'El reciclaje es un proceso fisicoquimico o mecanico que consiste en someter a una materia o un producto ya utilizado a un ciclo de tratamiento total o parcial para obtener una materia prima o un nuevo producto. Tambien se podria definir como la obtencion de materias primas a partir de desechos, introduciendolos de nuevo en el ciclo de vida y se produce ante la perspectiva del agotamiento de recursos naturales, macro economico y para eliminar de forma eficaz los desechos de los humanos que no necesitamos.', 20,'Aprender a reciclar', 1, 7,0,'false')
INSERT "ConvocatoriaSet"("Inicio","UbicacionLatitud","UbicacionLongitud","Descripcion","Quorum","Titulo","MovimientoId","CategoriaTematicaId","CantUsuariosConfirmados","Suspendida") VALUES('10/7/2012', -33, -56,'Aire y proteccion de la atmosfera', 20,'Lucha por el aire y proteccion de la atmosfera', 1, 3,0,'false')
INSERT "ConvocatoriaSet"("Inicio","UbicacionLatitud","UbicacionLongitud","Descripcion","Quorum","Titulo","MovimientoId","CategoriaTematicaId","CantUsuariosConfirmados","Suspendida") VALUES('11/8/2012', -30, -56,'Se llama cambio climatico a la modificacion del clima con respecto al historial climatico a una escala global o regional. Tales cambios se producen a muy diversas escalas de tiempo y sobre todos los parametros meteorologicos: temperatura, presion atmosferica, precipitaciones, nubosidad, etc. El termino suele usarse de manera poco apropiada, para hacer referencia tan solo a los cambios climaticos que suceden en el presente, utilizandolo como sinonimo de calentamiento global.', 20,'Marcha por cambio climatico', 1, 1,0,'false')
INSERT "ConvocatoriaSet"("Inicio","UbicacionLatitud","UbicacionLongitud","Descripcion","Quorum","Titulo","MovimientoId","CategoriaTematicaId","CantUsuariosConfirmados","Suspendida") VALUES('12/8/2012', -35, -56,'En varios paises del mundo han surgido grupos opuestos a los organismos geneticamente modificados, formados principalmente por ecologistas, asociaciones de derechos del consumidor, algunos cientificos y politicos, los cuales exigen el etiquetaje de estos, por sus preocupaciones sobre seguridad alimentaria, impactos ambientales, cambios culturales y dependencias económicas. Llaman a evitar este tipo de alimentos, cuya produccion involucraria daños a la salud, ambientales, economicos, sociales y problemas legales y eticos por concepto de patentes. De este modo, surge la polemica derivada entre sopesar las ventajas e inconvenientes del proceso. Es decir: el impacto beneficioso en cuanto a economia, estado medioambiental del ecosistema aledano al cultivo y en la salud del agricultor ha sido descrito, pero las dudas respecto a la posible aparicion de alergias, cambios en el perfil nutricional, dilucion del acervo genetico y difusion de resistencias a antibioticos tambien.', 20,'Protesta por alimentos transgenicos', 1, 13,0,'false')


INSERT "FuenteWEBSet"("Url","Tipo","MovimientoId","UrlDll") VALUES ('http://gdata.youtube.com/feeds/api/standardfeeds/top_rated','YOUTUBE',1,'https://dl.dropbox.com/u/85924014/YouTubeFeed.dll')
INSERT "FuenteWEBSet"("Url","Tipo","MovimientoId","UrlDll") VALUES ('http://www.portaldelmedioambiente.com/rss/articulos/','RSS',1,'https://dl.dropbox.com/u/85924014/RssFeed.dll')
INSERT "FuenteWEBSet"("Url","Tipo","MovimientoId","UrlDll") VALUES ('http://ep01.epimg.net/rss/elpais/portada.xml','RSS',1,'https://dl.dropbox.com/u/85924014/RssFeed.dll')
INSERT "FuenteWEBSet"("Url","Tipo","MovimientoId","UrlDll") VALUES ('http://www.portaldelmedioambiente.com/rss/noticias/','RSS',1,'https://dl.dropbox.com/u/85924014/RssFeed.dll')

INSERT "EspecificacionUsuarioSet"("Nombre","UbicacionLatitud" , "UbicacionLongitud", "Membership","MovimientoId","BajasContenido") VALUES ('Roberto',-33,-35,'roberto@gmail.com',1,0)

INSERT "ContenidoSet"("Ubicacion","Tipo","FechaPosteado","CategoriaTematicaId","Inadecuado","EspecificacionUsuarioId","Titulo","CantMeGusta","Habilitado") VALUES('http://www.youtube.com/watch?v=XV27NkxmAd4','Video','10/01/2012',1,0,1,'GreenPeace',0,'true')
INSERT "ContenidoSet"("Ubicacion","Tipo","FechaPosteado","CategoriaTematicaId","Inadecuado","EspecificacionUsuarioId","Titulo","CantMeGusta","Habilitado") VALUES('http://www.elpais.cr/files/news/image/detail/260312koala.jpg','Foto','10/01/2012',1,0,1,'Koala',0,'true')
INSERT "ContenidoSet"("Ubicacion","Tipo","FechaPosteado","CategoriaTematicaId","Inadecuado","EspecificacionUsuarioId","Titulo","CantMeGusta","Habilitado") VALUES('http://vimeo.com/42619545','Video','10/01/2012',12,0,1,'End Overfishing',0,'true')
INSERT "ContenidoSet"("Ubicacion","Tipo","FechaPosteado","CategoriaTematicaId","Inadecuado","EspecificacionUsuarioId","Titulo","CantMeGusta","Habilitado") VALUES('http://www.youtube.com/watch?v=03Hwbh7dipE','Video','10/01/2012',1,0,1,'Lista Roja, 15 animales en riesgo de extincion',0,'true')
INSERT "ContenidoSet"("Ubicacion","Tipo","FechaPosteado","CategoriaTematicaId","Inadecuado","EspecificacionUsuarioId","Titulo","CantMeGusta","Habilitado") VALUES('http://www.youtube.com/watch?v=e271FQ8nBNY','Video','10/01/2012',1,0,1,'Manifestación de Greenpeace Ley de Basura Cero ',0,'true')
INSERT "ContenidoSet"("Ubicacion","Tipo","FechaPosteado","CategoriaTematicaId","Inadecuado","EspecificacionUsuarioId","Titulo","CantMeGusta","Habilitado") VALUES('http://repoindignado.blob.core.windows.net/contenido1/04 - Historietas.mp3','Video','2012-06-05 23:31:43.277',12,0,1,'El Cuarteto de Nos - Historietas',0,'true')
INSERT "ContenidoSet"("Ubicacion","Tipo","FechaPosteado","CategoriaTematicaId","Inadecuado","EspecificacionUsuarioId","Titulo","CantMeGusta","Habilitado") VALUES('http://repoindignado.blob.core.windows.net/contenido1/mr_bean_en_el_dentista mp4.3gp','Video','2012-06-06 00:38:41.490',12,0,1,'Mr Bean en el doctor',0,'true')