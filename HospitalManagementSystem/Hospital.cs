using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem
{
    public class Hospital
    {
        public List<Doctor> Doctors { get;  set; }
        public List<Patient> Patients { get; set; }
        public List<HospitalRoom> Rooms { get; set; }
        public List<MedicalRecord> Records {get; set; }
        public Hospital() {
            Doctors = new List<Doctor>();
            Patients = new List<Patient>();
            Rooms = new List<HospitalRoom>();
            Records = new List<MedicalRecord>();
        }
        public void AddDoctor(Doctor doctor)
        {
            Doctors.Add(doctor);
            Console.WriteLine($"Лікар {doctor.Name} ({doctor.Specialization}) доданий до системи");
        }
        public void RegisterPatient(Patient patient)
        {
            Patients.Add(patient);
            Console.WriteLine($"Пацієнт {patient.Name}, {patient.Age} років, зареєстрований");
        }
        public void CreateRoom(HospitalRoom room)
        {
            Rooms.Add(room);
            Console.WriteLine($"Палата №{room.RoomNumber} створена (місткість: {room.Capacity})");
        }
        public void HospitalizePatient(int patientId, int roomNumber)
        {
            Patient foundPatient = null;

            foreach (var patient in Patients)
            {
                if(patient.Id == patientId)
                {
                    foundPatient = patient;
                    break;
                }
            }

            if(foundPatient == null)
            {
                Console.WriteLine($"Пацієнт з ID {patientId} не знайдений!");
                return;
            }

            HospitalRoom foundRoom = null;
            foreach (var room in Rooms)
            {
                if(room.RoomNumber == roomNumber)
                {
                    foundRoom = room;
                    break;
                }
            }

            if( foundRoom == null )
            {
                Console.WriteLine($"Палата №{roomNumber} не знайдена!");
                return;
            }
            foundRoom.AddPatient(foundPatient);
        }

        public void AddMedicalRecord(MedicalRecord record)
        {
            Records.Add(record);
            Console.WriteLine($"Медичний запис створено: {record.Patient.Name} -> {record.Doctor.Name}");
        }

        public List<MedicalRecord> GetPatientHistory(int patientId)
        {
            List<MedicalRecord> history = new List<MedicalRecord>();
            foreach(var record in Records)
            {
                if(record.Patient.Id == patientId)
                {
                    history.Add(record);
                }
            }
            return history;
        }
        public string GetStatistics()
        {
            int totalPatientsInRooms = 0;

            foreach(var room in Rooms)
            {
                totalPatientsInRooms += room.Patients.Count;
            }

            return $"\n=== СТАТИСТИКА ЛІКАРНІ ===\n" +
           $"Кількість лікарів: {Doctors.Count}\n" +
           $"Кількість зареєстрованих пацієнтів: {Patients.Count}\n" +
           $"Кількість палат: {Rooms.Count}\n" +
           $"Кількість пацієнтів у палатах: {totalPatientsInRooms}\n" +
           $"Кількість медичних записів: {Records.Count}\n";
        }
    }
}
