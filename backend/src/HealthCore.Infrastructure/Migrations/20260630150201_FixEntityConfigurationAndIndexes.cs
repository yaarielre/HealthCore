using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HealthCore.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixEntityConfigurationAndIndexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "Specialties",
                type: "bit",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.CreateIndex(
                name: "IX_UserActivityLogs_UserId_CreatedAt",
                table: "UserActivityLogs",
                columns: new[] { "UserId", "CreatedAt" },
                descending: new[] { false, true });

            migrationBuilder.CreateIndex(
                name: "IX_Patients_FirstName_LastName",
                table: "Patients",
                columns: new[] { "FirstName", "LastName" });

            migrationBuilder.CreateIndex(
                name: "IX_PasswordResetTokens_UserId_ExpiresAt",
                table: "PasswordResetTokens",
                columns: new[] { "UserId", "ExpiresAt" });

            migrationBuilder.CreateIndex(
                name: "IX_MedicalConsultations_CreatedAt",
                table: "MedicalConsultations",
                column: "CreatedAt",
                descending: new bool[0]);

            migrationBuilder.CreateIndex(
                name: "IX_MedicalConsultations_PatientId_CreatedAt",
                table: "MedicalConsultations",
                columns: new[] { "PatientId", "CreatedAt" },
                descending: new[] { false, true });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_DoctorId_AppointmentDate",
                table: "Appointments",
                columns: new[] { "DoctorId", "AppointmentDate" });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_PatientId_AppointmentDate",
                table: "Appointments",
                columns: new[] { "PatientId", "AppointmentDate" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserActivityLogs_UserId_CreatedAt",
                table: "UserActivityLogs");

            migrationBuilder.DropIndex(
                name: "IX_Patients_FirstName_LastName",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_PasswordResetTokens_UserId_ExpiresAt",
                table: "PasswordResetTokens");

            migrationBuilder.DropIndex(
                name: "IX_MedicalConsultations_CreatedAt",
                table: "MedicalConsultations");

            migrationBuilder.DropIndex(
                name: "IX_MedicalConsultations_PatientId_CreatedAt",
                table: "MedicalConsultations");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_DoctorId_AppointmentDate",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_PatientId_AppointmentDate",
                table: "Appointments");

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "Specialties",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: true);
        }
    }
}
