﻿using MediatR;
using CourseProject.Application.Dtos;

namespace CourseProject.Application.Requests.Commands;

public record UpdateSubscriberCommand(SubscriberForUpdateDto Subscriber) : IRequest<bool>;