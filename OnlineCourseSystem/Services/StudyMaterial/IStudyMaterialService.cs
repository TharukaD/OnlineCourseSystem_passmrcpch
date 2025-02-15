﻿namespace OnlineCourseSystem.Services.StudyMaterial;
using OnlineCourseSystem.Entities;
public interface IStudyMaterialService
{
	Task<IEnumerable<StudyMaterialType>> GetAllTypes();
	Task<StudyMaterial?> GetById(int id);
	Task<IEnumerable<StudyMaterial>> GetAll(int? categoryId = null);
	Task<IEnumerable<StudyMaterial>> GetAllPublished(int? categoryId = null);
	Task<bool> Add(StudyMaterial material);
	Task<bool> Update(StudyMaterial material);
	Task<bool> Delete(int id);
	Task<bool> IsDublicate(int id, string name);
}
