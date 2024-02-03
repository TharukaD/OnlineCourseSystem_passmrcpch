namespace OnlineCourseSystem.Services.StudyMaterialTopic;
using Microsoft.EntityFrameworkCore;
using OnlineCourseSystem.Data;
using OnlineCourseSystem.Entities;
public class StudyMaterialTopicService : IStudyMaterialTopicService
{
	private ApplicationDbContext _context;
	public StudyMaterialTopicService(ApplicationDbContext context)
	{
		_context = context;
	}

	public async Task<StudyMaterialTopic?> GetById(int id)
	{
		return await _context.StudyMaterialTopics.SingleOrDefaultAsync(r => r.Id == id);
	}

	public async Task<IEnumerable<StudyMaterialTopic>> GetAll()
	{
		return await _context.StudyMaterialTopics
			.OrderBy(r => r.Priority).ToListAsync();
	}

	public async Task<IEnumerable<StudyMaterialTopic>> GetAllWithCategories()
	{
		var result = await _context.StudyMaterialTopics
			.Include(r => r.StudyMaterialCategories)
			.OrderBy(r => r.Priority).ToListAsync();

		return result;
	}

	public async Task<bool> Add(StudyMaterialTopic materialTopic)
	{
		_context.StudyMaterialTopics.Add(materialTopic);
		await _context.SaveChangesAsync();
		return true;
	}

	public async Task<bool> Delete(int id)
	{
		var materialTopic = await _context.StudyMaterialTopics.FindAsync(id);
		if (materialTopic == null)
		{
			return false;
		}
		_context.StudyMaterialTopics.Remove(materialTopic);
		await _context.SaveChangesAsync();
		return true;
	}

	public async Task<bool> IsDublicate(int id, string name)
	{
		if (id == 0)
		{
			if (await _context.StudyMaterialTopics.FirstOrDefaultAsync(r => r.Name == name) != null)
				return true;
		}
		else
		{
			if (await _context.StudyMaterialTopics.FirstOrDefaultAsync(r => r.Name == name && r.Id != id) != null)
				return true;
		}
		return false;
	}

	public async Task<bool> Update(StudyMaterialTopic topic)
	{
		_context.Update(topic);
		await _context.SaveChangesAsync();
		return true;
	}

	public async Task<bool> HasAssignedToStudyMaterial(int topicId)
	{
		var firstCategory = await _context.StudyMaterialCategories.FirstOrDefaultAsync(r => r.TopicId == topicId);
		if (firstCategory != null)
			return true;
		return false;
	}
}
