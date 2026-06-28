using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HealthCore.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenameSpanishToEnglishAndAddConstraints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Doctors_DoctorId1",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_PasswordResetTokens_Users_UserId",
                table: "PasswordResetTokens");

            migrationBuilder.DropForeignKey(
                name: "FK_UserActivityLogs_Users_UserId",
                table: "UserActivityLogs");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_DoctorId1",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "Cedula",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "DoctorId1",
                table: "Appointments");

            migrationBuilder.RenameColumn(
                name: "UpdateAt",
                table: "VitalSigns",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "Temperatura",
                table: "VitalSigns",
                newName: "Temperature");

            migrationBuilder.RenameColumn(
                name: "SaturacionOxigeno",
                table: "VitalSigns",
                newName: "OxygenSaturation");

            migrationBuilder.RenameColumn(
                name: "PresionArterial",
                table: "VitalSigns",
                newName: "BloodPressure");

            migrationBuilder.RenameColumn(
                name: "Peso",
                table: "VitalSigns",
                newName: "Weight");

            migrationBuilder.RenameColumn(
                name: "FrecuenciaCardiaca",
                table: "VitalSigns",
                newName: "HeartRate");

            migrationBuilder.RenameColumn(
                name: "Estatura",
                table: "VitalSigns",
                newName: "Height");

            migrationBuilder.RenameColumn(
                name: "UpdateAt",
                table: "Users",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "UpdateAt",
                table: "UserActivityLogs",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "UpdateAt",
                table: "Specialties",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "UpdateAt",
                table: "Prescriptions",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "Medicamento",
                table: "Prescriptions",
                newName: "Medication");

            migrationBuilder.RenameColumn(
                name: "Indicaciones",
                table: "Prescriptions",
                newName: "Instructions");

            migrationBuilder.RenameColumn(
                name: "Frecuencia",
                table: "Prescriptions",
                newName: "Frequency");

            migrationBuilder.RenameColumn(
                name: "Duracion",
                table: "Prescriptions",
                newName: "Duration");

            migrationBuilder.RenameColumn(
                name: "Dosis",
                table: "Prescriptions",
                newName: "Dosage");

            migrationBuilder.RenameColumn(
                name: "UpdateAt",
                table: "Patients",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "UpdateAt",
                table: "PasswordResetTokens",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "UpdateAt",
                table: "MedicalConsultations",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "Tratamiento",
                table: "MedicalConsultations",
                newName: "Treatment");

            migrationBuilder.RenameColumn(
                name: "Sintomas",
                table: "MedicalConsultations",
                newName: "Symptoms");

            migrationBuilder.RenameColumn(
                name: "Observaciones",
                table: "MedicalConsultations",
                newName: "Observations");

            migrationBuilder.RenameColumn(
                name: "NotasInternas",
                table: "MedicalConsultations",
                newName: "InternalNotes");

            migrationBuilder.RenameColumn(
                name: "MotivoConsulta",
                table: "MedicalConsultations",
                newName: "ReasonForVisit");

            migrationBuilder.RenameColumn(
                name: "Diagnostico",
                table: "MedicalConsultations",
                newName: "Diagnosis");

            migrationBuilder.RenameColumn(
                name: "UpdateAt",
                table: "Doctors",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "UpdateAt",
                table: "Appointments",
                newName: "UpdatedAt");

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "Users",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "PasswordHash",
                table: "Users",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Users",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "IdNumber",
                table: "Users",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Users",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Module",
                table: "UserActivityLogs",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "IpAddress",
                table: "UserActivityLogs",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Details",
                table: "UserActivityLogs",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Action",
                table: "UserActivityLogs",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "Patients",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Patients",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Patients",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Patients",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "IdNumber",
                table: "Patients",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Token",
                table: "PasswordResetTokens",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_IdNumber",
                table: "Users",
                column: "IdNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Role",
                table: "Users",
                column: "Role");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Status",
                table: "Users",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_UserActivityLogs_Action",
                table: "UserActivityLogs",
                column: "Action");

            migrationBuilder.CreateIndex(
                name: "IX_UserActivityLogs_Module",
                table: "UserActivityLogs",
                column: "Module");

            migrationBuilder.CreateIndex(
                name: "IX_PasswordResetTokens_ExpiresAt",
                table: "PasswordResetTokens",
                column: "ExpiresAt");

            migrationBuilder.CreateIndex(
                name: "IX_PasswordResetTokens_Token",
                table: "PasswordResetTokens",
                column: "Token");

            migrationBuilder.AddForeignKey(
                name: "FK_PasswordResetTokens_Users_UserId",
                table: "PasswordResetTokens",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserActivityLogs_Users_UserId",
                table: "UserActivityLogs",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PasswordResetTokens_Users_UserId",
                table: "PasswordResetTokens");

            migrationBuilder.DropForeignKey(
                name: "FK_UserActivityLogs_Users_UserId",
                table: "UserActivityLogs");

            migrationBuilder.DropIndex(
                name: "IX_Users_Email",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_IdNumber",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_Role",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_Status",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_UserActivityLogs_Action",
                table: "UserActivityLogs");

            migrationBuilder.DropIndex(
                name: "IX_UserActivityLogs_Module",
                table: "UserActivityLogs");

            migrationBuilder.DropIndex(
                name: "IX_PasswordResetTokens_ExpiresAt",
                table: "PasswordResetTokens");

            migrationBuilder.DropIndex(
                name: "IX_PasswordResetTokens_Token",
                table: "PasswordResetTokens");

            migrationBuilder.DropColumn(
                name: "IdNumber",
                table: "Patients");

            migrationBuilder.RenameColumn(
                name: "Weight",
                table: "VitalSigns",
                newName: "Peso");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "VitalSigns",
                newName: "UpdateAt");

            migrationBuilder.RenameColumn(
                name: "Temperature",
                table: "VitalSigns",
                newName: "Temperatura");

            migrationBuilder.RenameColumn(
                name: "OxygenSaturation",
                table: "VitalSigns",
                newName: "SaturacionOxigeno");

            migrationBuilder.RenameColumn(
                name: "Height",
                table: "VitalSigns",
                newName: "Estatura");

            migrationBuilder.RenameColumn(
                name: "HeartRate",
                table: "VitalSigns",
                newName: "FrecuenciaCardiaca");

            migrationBuilder.RenameColumn(
                name: "BloodPressure",
                table: "VitalSigns",
                newName: "PresionArterial");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "Users",
                newName: "UpdateAt");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "UserActivityLogs",
                newName: "UpdateAt");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "Specialties",
                newName: "UpdateAt");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "Prescriptions",
                newName: "UpdateAt");

            migrationBuilder.RenameColumn(
                name: "Medication",
                table: "Prescriptions",
                newName: "Medicamento");

            migrationBuilder.RenameColumn(
                name: "Instructions",
                table: "Prescriptions",
                newName: "Indicaciones");

            migrationBuilder.RenameColumn(
                name: "Frequency",
                table: "Prescriptions",
                newName: "Frecuencia");

            migrationBuilder.RenameColumn(
                name: "Duration",
                table: "Prescriptions",
                newName: "Duracion");

            migrationBuilder.RenameColumn(
                name: "Dosage",
                table: "Prescriptions",
                newName: "Dosis");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "Patients",
                newName: "UpdateAt");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "PasswordResetTokens",
                newName: "UpdateAt");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "MedicalConsultations",
                newName: "UpdateAt");

            migrationBuilder.RenameColumn(
                name: "Treatment",
                table: "MedicalConsultations",
                newName: "Tratamiento");

            migrationBuilder.RenameColumn(
                name: "Symptoms",
                table: "MedicalConsultations",
                newName: "Sintomas");

            migrationBuilder.RenameColumn(
                name: "ReasonForVisit",
                table: "MedicalConsultations",
                newName: "MotivoConsulta");

            migrationBuilder.RenameColumn(
                name: "Observations",
                table: "MedicalConsultations",
                newName: "Observaciones");

            migrationBuilder.RenameColumn(
                name: "InternalNotes",
                table: "MedicalConsultations",
                newName: "NotasInternas");

            migrationBuilder.RenameColumn(
                name: "Diagnosis",
                table: "MedicalConsultations",
                newName: "Diagnostico");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "Doctors",
                newName: "UpdateAt");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "Appointments",
                newName: "UpdateAt");

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "PasswordHash",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "IdNumber",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150);

            migrationBuilder.AlterColumn<string>(
                name: "Module",
                table: "UserActivityLogs",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "IpAddress",
                table: "UserActivityLogs",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Details",
                table: "UserActivityLogs",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(2000)",
                oldMaxLength: 2000,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Action",
                table: "UserActivityLogs",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250);

            migrationBuilder.AddColumn<string>(
                name: "Cedula",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Token",
                table: "PasswordResetTokens",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AddColumn<Guid>(
                name: "DoctorId1",
                table: "Appointments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_DoctorId1",
                table: "Appointments",
                column: "DoctorId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Doctors_DoctorId1",
                table: "Appointments",
                column: "DoctorId1",
                principalTable: "Doctors",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PasswordResetTokens_Users_UserId",
                table: "PasswordResetTokens",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserActivityLogs_Users_UserId",
                table: "UserActivityLogs",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
