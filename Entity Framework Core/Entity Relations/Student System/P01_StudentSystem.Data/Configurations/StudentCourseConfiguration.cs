﻿namespace P01_StudentSystem.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models;

    public class StudentCourseConfiguration : IEntityTypeConfiguration<StudentCourse>
    {
        public void Configure(EntityTypeBuilder<StudentCourse> studentCourse)
        {
            studentCourse.HasKey(sc => new { sc.CourseId, sc.StudentId });

            studentCourse
                .HasOne(sc => sc.Student)
                .WithMany(s => s.Courses)
                .HasForeignKey(sc => sc.StudentId);

            studentCourse
                .HasOne(sc => sc.Course)
                .WithMany(c => c.Students)
                .HasForeignKey(sc => sc.CourseId);
        }
    }
}
