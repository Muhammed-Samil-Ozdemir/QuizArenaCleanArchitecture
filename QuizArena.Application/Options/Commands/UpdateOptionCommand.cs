using FluentValidation;
using QuizArena.Application.Abstractions;
using QuizArena.Application.Common.Results;
using QuizArena.Domain.Abstractions;
using QuizArena.Domain.Options;

namespace QuizArena.Application.Options.Commands;

public sealed record UpdateOptionCommand(Guid Id, string Text, bool IsCorrect) : ICommand<UpdateOptionResponse>;

public sealed class UpdateOptionCommandValidator : AbstractValidator<UpdateOptionCommand>
{
    public UpdateOptionCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEqual(Guid.Empty).WithMessage("Id cannot be empty.");

        RuleFor(x => x.Text)
            .NotEmpty().WithMessage("Option text cannot be empty.")
            .MaximumLength(200).WithMessage("Option text cannot exceed 200 characters.");
    }
}

internal sealed class UpdateOptionCommandHandler(
    IOptionRepository repository,
    IUnitOfWork unitOfWork) : ICommandHandler<UpdateOptionCommand, UpdateOptionResponse>
{
    public async Task<Result<UpdateOptionResponse>> Handle(UpdateOptionCommand request, CancellationToken cancellationToken)
    {
        var option = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (option is null)
            return Result<UpdateOptionResponse>.NotFound("Option not found.");

        bool optionExist = await repository.AnyAsync(x => x.Id != request.Id && x.Text == request.Text, cancellationToken);
        if (optionExist)
            return Result<UpdateOptionResponse>.Conflict("An option with the same text already exists.");

        option.Text = request.Text;
        option.IsCorrect = request.IsCorrect;

        repository.Update(option);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<UpdateOptionResponse>.Success(new UpdateOptionResponse(option.Id));
    }
}

public sealed record UpdateOptionResponse(Guid Id);