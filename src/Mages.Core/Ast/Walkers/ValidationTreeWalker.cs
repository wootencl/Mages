﻿namespace Mages.Core.Ast.Walkers
{
    using Mages.Core.Ast.Expressions;
    using Mages.Core.Ast.Statements;
    using System.Collections.Generic;

    /// <summary>
    /// Represents the walker to validate the AST.
    /// </summary>
    public sealed class ValidationTreeWalker : ITreeWalker, IValidationContext
    {
        #region Fields

        private readonly List<ParseError> _errors;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new validation tree walker with the list of errors
        /// to populate.
        /// </summary>
        /// <param name="errors">The list to populate.</param>
        public ValidationTreeWalker(List<ParseError> errors)
        {
            _errors = errors;
        }

        #endregion

        #region Visitors

        void ITreeWalker.Visit(VarStatement statement)
        {
            statement.Validate(this);
        }

        void ITreeWalker.Visit(BlockStatement block)
        {
            block.Validate(this);

            foreach (var child in block.Statements)
            {
                child.Accept(this);
            }
        }

        void ITreeWalker.Visit(SimpleStatement statement)
        {
            statement.Validate(this);
            statement.Expression.Accept(this);
        }

        void ITreeWalker.Visit(EmptyExpression expression)
        {
            expression.Validate(this);
        }

        void ITreeWalker.Visit(ConstantExpression expression)
        {
            expression.Validate(this);
        }

        void ITreeWalker.Visit(ArgumentsExpression expression)
        {
            expression.Validate(this);

            foreach (var argument in expression.Arguments)
            {
                argument.Accept(this);
            }
        }

        void ITreeWalker.Visit(AssignmentExpression expression)
        {
            expression.Validate(this);
            expression.Variable.Accept(this);
            expression.Value.Accept(this);
        }

        void ITreeWalker.Visit(BinaryExpression expression)
        {
            expression.Validate(this);
            expression.LValue.Accept(this);
            expression.RValue.Accept(this);
        }

        void ITreeWalker.Visit(PreUnaryExpression expression)
        {
            expression.Validate(this);
            expression.Value.Accept(this);
        }

        void ITreeWalker.Visit(PostUnaryExpression expression)
        {
            expression.Validate(this);
            expression.Value.Accept(this);
        }

        void ITreeWalker.Visit(RangeExpression expression)
        {
            expression.Validate(this);
            expression.From.Accept(this);
            expression.Step.Accept(this);
            expression.To.Accept(this);
        }

        void ITreeWalker.Visit(ConditionalExpression expression)
        {
            expression.Validate(this);
            expression.Condition.Accept(this);
            expression.Primary.Accept(this);
            expression.Secondary.Accept(this);
        }

        void ITreeWalker.Visit(CallExpression expression)
        {
            expression.Validate(this);
            expression.Function.Accept(this);
            expression.Arguments.Accept(this);
        }

        void ITreeWalker.Visit(ObjectExpression expression)
        {
            expression.Validate(this);

            foreach (var value in expression.Values)
            {
                value.Accept(this);
            }
        }

        void ITreeWalker.Visit(PropertyExpression expression)
        {
            expression.Validate(this);
            expression.Name.Accept(this);
            expression.Value.Accept(this);
        }

        void ITreeWalker.Visit(MatrixExpression expression)
        {
            expression.Validate(this);

            foreach (var row in expression.Values)
            {
                foreach (var value in row)
                {
                    value.Accept(this);
                }
            }
        }

        void ITreeWalker.Visit(FunctionExpression expression)
        {
            expression.Validate(this);
            expression.Parameters.Accept(this);
            expression.Body.Accept(this);
        }

        void ITreeWalker.Visit(InvalidExpression expression)
        {
            expression.Validate(this);
        }

        void ITreeWalker.Visit(IdentifierExpression expression)
        {
            expression.Validate(this);
        }

        void ITreeWalker.Visit(MemberExpression expression)
        {
            expression.Validate(this);
            expression.Object.Accept(this);
            expression.Member.Accept(this);
        }

        void ITreeWalker.Visit(ParameterExpression expression)
        {
            expression.Validate(this);
            
            foreach (var parameter in expression.Expressions)
            {
                parameter.Accept(this);
            }
        }

        void ITreeWalker.Visit(VariableExpression expression)
        {
            expression.Validate(this);
        }

        #endregion

        #region Reporting

        void IValidationContext.Report(ParseError error)
        {
            _errors.Add(error);
        }

        #endregion
    }
}