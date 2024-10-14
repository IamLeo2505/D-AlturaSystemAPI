create proc pA_lista_credito
as
begin
     select 
	 IdCr�dito, PlazoPago, TasaInteres, Monto, FechaInicio
	 from Cr�dito
end


create proc pA_guardar_credito(
@IdCr�dito int,
@PlazoPago int,
@TasaInteres int,
@Monto decimal(10, 0),
@FechaInicio date
) as 
begin
     insert into Cr�dito(IdCr�dito, PlazoPago, TasaInteres, Monto, FechaInicio)
	 values (@IdCr�dito, @PlazoPago, @TasaInteres, @Monto, @FechaInicio)
end



create proc pA_editar_credito(
@IdCr�dito int,
@PlazoPago int null,
@TasaInteres int null,
@Monto decimal(10, 0) null
) as 
begin

update Cr�dito set

PlazoPago = isnull(@PlazoPago, PlazoPago),
TasaInteres = isnull(@TasaInteres, TasaInteres),
Monto = isnull(@Monto, Monto)
where IdCr�dito = @IdCr�dito
end

create proc pA_eliminar_credito(
@IdCr�dito int
)
as 
begin
delete from Cr�dito where IdCr�dito = @IdCr�dito
end