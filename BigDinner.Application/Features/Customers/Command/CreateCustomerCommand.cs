using BigDinner.Application.Common.Abstractions.Authentication;
using BigDinner.Application.Common.Abstractions.Repository;
using BigDinner.Application.Features.Menus.Command;
using BigDinner.Domain.Models.Base;
using BigDinner.Domain.Models.Customers;
using BigDinner.Domain.Models.Menus;
using Microsoft.AspNetCore.Identity;

namespace BigDinner.Application.Features.Customers.Command;

public record CreateCustomerCommand(string Name, string Email, string Phone, Address Address, string Password)
    : IRequest<Response<AuthResponse>>;


public sealed class CreateCustomerCommandHandler : ResponseHandler,
IRequestHandler<CreateCustomerCommand, Response<AuthResponse>>
{
    private readonly IMapper _mapper;
    private readonly ICustomerRepository _customerRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<ApplicationUser> _userManager;
    public readonly IJwtTokenGenerator _jwtTokenGenerator;

    public CreateCustomerCommandHandler(
        IMapper mapper,
        ICustomerRepository customerRepository,
        IUnitOfWork unitOfWork,
        UserManager<ApplicationUser> userManager,
        IJwtTokenGenerator jwtTokenGenerator)
    {
        _mapper = mapper;
        _customerRepository = customerRepository;
        _unitOfWork = unitOfWork;
        _userManager = userManager;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<Response<AuthResponse>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var beginTransaction = _unitOfWork.BeginTransaction();
        try
        {
            var userByEmail = await _userManager.FindByEmailAsync(request.Email);
            if (userByEmail is not null)
                return BadRequest<AuthResponse>("Email is already exits");

            var userByUsername = await _userManager.FindByEmailAsync(request.Name);
            if (userByUsername is not null)
                return BadRequest<AuthResponse>("username is already exits");

            var customer = Customer.Create(request.Name, new Email(request.Email), request.Phone, request.Address);

            _customerRepository.Add(customer);

            var identityCustomer = IdentityCustomer.MapToIdentity(customer);

            var createUserResult = await _userManager.CreateAsync(identityCustomer, request.Password);
            if (!createUserResult.Succeeded)
            {
                throw new Exception("creation user is faild");
            }

            await _unitOfWork.CompleteAsync();

            beginTransaction.Commit();

            var authModel = await _jwtTokenGenerator.CreateAuthModel(identityCustomer);

            return Created(authModel);
        }
        catch (Exception ex)
        {
            beginTransaction.Rollback();
            throw;
        }

    }
}